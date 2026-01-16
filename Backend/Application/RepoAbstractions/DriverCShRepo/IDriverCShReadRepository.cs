using FormulaOne.Entities;

namespace FormulaOne.Application.RepoAbstractions.DriverCShRepo
{
    public interface IDriverCShReadRepository
    {
        Task<IEnumerable<DriverChampionship>> GetDriversTableBySeason(Season season);
    }
}
