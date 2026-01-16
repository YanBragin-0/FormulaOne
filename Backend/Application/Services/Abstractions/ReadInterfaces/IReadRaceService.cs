using FormulaOne.Application.Dto.Response;
using FormulaOne.Shared;

namespace FormulaOne.Application.Services.Abstractions.ReadInterfaces
{
    public interface IReadRaceService 
    {
        Task<Result<IEnumerable<RaceResponseDto>>> GetAllRaces();
        Task<Result<RaceResponseDto>> GetRaceByCountry(string Country);
    }
}
