using FormulaOne.Application.Dto.ElasticDto;

namespace FormulaOne.Application.Services.ElasticSearch
{
    public interface ISearchService
    {
        Task<List<SearchResponseDto>> SearchAsync(string query);
    }
}
