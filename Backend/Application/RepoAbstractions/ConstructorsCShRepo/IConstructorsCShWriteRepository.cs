using FormulaOne.Entities;

namespace FormulaOne.Application.RepoAbstractions.ConstructorsCShRepo
{
    public interface IConstructorsCShWriteRepository
    {
        Task AddTeamToTable(ConstructorsChampionship team);
    }
}
