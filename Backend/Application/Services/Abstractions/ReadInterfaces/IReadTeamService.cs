using FormulaOne.Application.Dto.Response;
using FormulaOne.Shared;

namespace FormulaOne.Application.Services.Abstractions.ReadInterfaces
{
    public interface IReadTeamService
    {
        Task<Result<IEnumerable<TeamResponseDto>>> GetAllTeams();
        Task<Result<TeamResponseDto>> GetTeamByName(string TeamName);
    }
}
