namespace FormulaOne.Application.Dto.ElasticDto
{
    public sealed class SearchDocument
    {
        public string Id { get; set; } = null!;
        public string DocType { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string SearchText { get; set; } = string.Empty;
    }
}
