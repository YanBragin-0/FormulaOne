using FormulaOne.Entities;

namespace FormulaOne.Application.RepoAbstractions.CarsRepo
{
    public interface ICarsReadRepository
    {
        Task<IEnumerable<Cars>> GetAllCars();
        Task<Cars> GetTeamCars(Teams team,Season season);
    }
}
