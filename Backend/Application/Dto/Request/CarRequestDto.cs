namespace FormulaOne.Application.Dto.Request
{
    public record CarRequestDto(Guid TeamId,Guid SeasonId,string Title,string Description);
}
