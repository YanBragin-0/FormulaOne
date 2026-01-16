using FormulaOne.Application.Dto.Request;
using FormulaOne.Application.Dto.Response;
using FormulaOne.Entities;
using FormulaOne.Shared;

namespace FormulaOne.Application.Services.Abstractions.ReadInterfaces
{
    public interface IReadCarService
    {
        Task<Result<IEnumerable<CarsResponseDto>>> GetAllCars();
        Task<Result<CarsResponseDto>> GetTeamCar(TeamRequestDto Team,SeasonRequestDto season);
    }
}
