using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.Core.Search;
using Elastic.Clients.Elasticsearch.QueryDsl;
using FormulaOne.Application.Dto.ElasticDto;

namespace FormulaOne.Application.Services.ElasticSearch
{
    public class SearchService(ElasticsearchClient client) : ISearchService
    {
        private readonly ElasticsearchClient _elastc = client;
        public async Task<List<SearchResponseDto>> SearchAsync(string query)
        {
            var fields = new[] { new Field("title^5"), new Field("description^2"), new Field("searchText^3") };
            var response = await _elastc.SearchAsync<SearchDocument>(s => s
                    .Index("global")
                    .Size(10)
                    .Query(q =>
                        q.MultiMatch(m => m
                            .Query(query)
                                .Fields(fields).Type(TextQueryType.BestFields)
                        )
                    )
                    .Collapse(c => c.Field("title.keyword"))

            );
            return response.Hits.Where(h => h.Source != null).Select(h => MapHit(h)).ToList();
        }
        private SearchResponseDto MapHit(Hit<SearchDocument> hit)
        {
            var response = new SearchResponseDto
            {
                Id = hit.Id,
                Type = hit.Source!.DocType,
                Title = hit.Source.Title,
                Description = hit.Source.Description,
                Score = hit.Score ?? 0
            };
            return response;
        }
    }
}
