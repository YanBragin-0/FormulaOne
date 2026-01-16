namespace FormulaOne.Application.Dto.Request
{
    public record DriverRequestDto(Guid TeamId,string Name,short Age,string Country,string? Biography);
}
