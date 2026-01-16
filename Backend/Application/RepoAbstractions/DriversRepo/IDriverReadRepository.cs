using FormulaOne.Entities;

namespace FormulaOne.Application.RepoAbstractions.DriversRepo
{
    public interface IDriverReadRepository
    {
        Task<Drivers> GetDriverById(Guid DriverId);
        Task<IEnumerable<Drivers>> GetAllDrivers();
        Task<IEnumerable<Drivers>> GetTeamDrivers(Teams Team);
    }
}
