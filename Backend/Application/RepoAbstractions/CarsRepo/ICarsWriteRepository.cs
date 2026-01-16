using FormulaOne.Entities;

namespace FormulaOne.Application.RepoAbstractions.CarsRepo
{
    public interface ICarsWriteRepository
    {
        Task AddCar(Cars car);
        Task DeleteById(Guid Id);
        Task<bool> CheckSeasonValidCount(Guid SeasonId,Guid TeamId);
    }
}
