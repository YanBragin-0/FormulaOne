using FormulaOne.Entities;

namespace FormulaOne.Application.RepoAbstractions.DriversRepo
{
    public interface IDriverWriteRepository
    {
        Task AddDriver(Drivers Driver);
        Task DeleteDriverById(Guid Id);

    }
}
