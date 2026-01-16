using FormulaOne.Application.Dto.Response;
using FormulaOne.Shared;

namespace FormulaOne.Application.Services.Abstractions.ReadInterfaces
{
    public interface IReadConstrCShService
    {
        Task<Result<IEnumerable<TeamTableResponseDto>>> GetTeableBySeason(short year);
    }
}
