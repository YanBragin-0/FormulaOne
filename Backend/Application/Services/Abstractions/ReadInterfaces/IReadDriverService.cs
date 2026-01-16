using FormulaOne.Application.Dto.Response;
using FormulaOne.Shared;

namespace FormulaOne.Application.Services.Abstractions.ReadInterfaces
{
    public interface IReadDriverService
    {
        Task<Result<IEnumerable<DriverResponseDto>>> GetAllDrivers();
        Task<Result<IEnumerable<DriverResponseDto>>> GetTeamDrivers(string TeamName);
    }
}
