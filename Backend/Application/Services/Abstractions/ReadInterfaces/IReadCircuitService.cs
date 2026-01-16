using FormulaOne.Application.Dto.Response;
using FormulaOne.Shared;

namespace FormulaOne.Application.Services.Abstractions.ReadInterfaces
{
    public interface IReadCircuitService
    {
        Task<Result<IEnumerable<CircuitResponseDto>>> GetAllCircuits();
        Task<Result<CircuitResponseDto>> GetById(Guid Id);
    }
}
