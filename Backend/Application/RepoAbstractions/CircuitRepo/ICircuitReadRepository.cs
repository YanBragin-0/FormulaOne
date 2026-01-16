using FormulaOne.Entities;

namespace FormulaOne.Application.RepoAbstractions.CircuitRepo
{
    public interface ICircuitReadRepository
    {
        Task<IEnumerable<RaceCircuit>> GetAllCircuit();
        Task<RaceCircuit> GetCircuitById(Guid CircuitId);
        Task<RaceCircuit> GetByCountry(string Country);
    }
}
