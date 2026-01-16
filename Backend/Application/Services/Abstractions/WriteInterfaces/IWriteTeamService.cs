using FormulaOne.Application.Dto.Request;

namespace FormulaOne.Application.Services.Abstractions.WriteInterfaces
{
    public interface IWriteTeamService
    {
        Task AddTeam(TeamRequestDto requestDto);
        Task AddBiography(Guid IdTeam,TeamRequestDto requestDto);
        Task DeleteTeam(Guid TeamId);

    }
}
