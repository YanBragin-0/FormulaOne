using FormulaOne.Entities;

namespace FormulaOne.Application.RepoAbstractions.CircuitRepo
{
    public interface ICircuitWriteRepository
    {
        Task<bool> AddCircuit(RaceCircuit circuit);
        Task DeleteCircuitById(Guid Id);
    }
}
