using FormulaOne.Application.Dto.Request;

namespace FormulaOne.Application.Services.Abstractions.WriteInterfaces
{
    public interface IWriteDriverCShService
    {
        Task AddDriverCShRecord(DriverCShRequestDto requestDto);
    }
}
