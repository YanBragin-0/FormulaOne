using FormulaOne.Application.Dto.Request;
using FormulaOne.Shared;
using System.Runtime.CompilerServices;

namespace FormulaOne.Application.Services.Abstractions.WriteInterfaces
{
    public interface IWriteCircuitService
    {
        Task<Result<bool>> AddCircuit(CircuitRequestDto circuitRequest);
        Task RemoveCircuit(Guid Id);
    }
}
