namespace FormulaOne.Application.Dto.Request
{
    public record ConstrCShRequestDto(Guid SeasonId,Guid TeamId,string TeamName,int Points,short year);

}
