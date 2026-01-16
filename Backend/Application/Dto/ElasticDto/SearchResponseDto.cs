namespace FormulaOne.Application.Dto.ElasticDto
{
    public class SearchResponseDto
    {
        public string Id { get; set; } = null!;
        public string Type { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Title { get; set; } = null!;
        public double Score { get; set; }
    }
}
