using FormulaOne.Entities;

namespace FormulaOne.Application.RepoAbstractions.TeamsRepo
{
    public interface ITeamWriteRepository
    {
        Task AddTeam(Teams Team);
        Task DeleteTeamById(Guid Id);
        void AddTeamBiography(Teams team,string Biography);
        Task<Teams> GetById(Guid Id);
    }
}
