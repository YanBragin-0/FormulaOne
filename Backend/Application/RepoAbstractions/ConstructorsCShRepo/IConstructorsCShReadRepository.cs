using FormulaOne.Entities;

namespace FormulaOne.Application.RepoAbstractions.ConstructorsCShRepo
{
    public interface IConstructorsCShReadRepository
    {
        Task<IEnumerable<ConstructorsChampionship>> GetTableBySeason(Season season);

    }
}
