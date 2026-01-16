namespace FormulaOne.Application.Dto.Request
{
    public record DriverCShRequestDto(Guid SeasonId,Guid DriverId,string DriverName,string TeamName,int Points,short year);
}
