using FormulaOne.Application.Dto.Response;
using FormulaOne.Shared;

namespace FormulaOne.Application.Services.Abstractions.ReadInterfaces
{
    public interface IReadDriverCShService
    {
        Task<Result<IEnumerable<DriverTableResponse>>> GetDriversTable(short year);
    }
}
