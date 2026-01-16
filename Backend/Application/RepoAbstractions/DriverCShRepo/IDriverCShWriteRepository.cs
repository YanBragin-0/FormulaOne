using FormulaOne.Entities;

namespace FormulaOne.Application.RepoAbstractions.DriverCShRepo
{
    public interface IDriverCShWriteRepository
    {
        Task AddDriverToTable(DriverChampionship driver);
    }
}
