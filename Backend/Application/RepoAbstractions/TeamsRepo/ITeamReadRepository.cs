using FormulaOne.Entities;

namespace FormulaOne.Application.RepoAbstractions.TeamsRepo
{
    public interface ITeamReadRepository
    {
        Task<IEnumerable<Teams>> GetAllTeams();
        Task<Teams> GetTeamById(Guid TeamId);
        Task<Teams> GetTeamByName(string Name);

    }
}
