using FormulaOne.Entities;

namespace FormulaOne.Application.RepoAbstractions.RaceRepo
{
    public interface IRaceWriteRepository
    {
        Task AddRace(Race race);
        Task DeleteByIdRace(Guid Id);

    }
}
