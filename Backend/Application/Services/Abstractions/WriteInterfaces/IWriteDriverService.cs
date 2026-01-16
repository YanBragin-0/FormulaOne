using FormulaOne.Application.Dto.Request;

namespace FormulaOne.Application.Services.Abstractions.WriteInterfaces
{
    public interface IWriteDriverService
    {
        Task AddDriver(DriverRequestDto requestDto);
        Task DeleteDriver(Guid DriverId);
    }
}
