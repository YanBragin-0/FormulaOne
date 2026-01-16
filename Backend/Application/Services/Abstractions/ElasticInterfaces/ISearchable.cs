using FormulaOne.Application.Dto.ElasticDto;

namespace FormulaOne.Application.Services.Abstractions.ElasticInterfaces
{
    public interface ISearchable
    {
        public string Id { get; set; }
        public string DocType { get; }
        public string Title { get; }
        public string Description { get; }
    }
}
