namespace FormulaOne.Application.Dto.Request
{
    public record RaceRequestDto(Guid CircuitId,string Title,DateTime? DateTime);
    public record RaceRequestDtoWithCache(string Country,string CircuitName);
}
