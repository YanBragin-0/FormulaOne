using FormulaOne.Entities;

namespace FormulaOne.Application.RepoAbstractions.RaceRepo
{
    public interface IRaceReadRepository
    {
        Task<IEnumerable<Race>> GetAllRaces();
        Task<Race> GetRaceById(Guid Id);

    }
}
