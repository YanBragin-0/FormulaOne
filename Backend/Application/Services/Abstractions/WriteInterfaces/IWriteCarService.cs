using FormulaOne.Application.Dto.Request;
using FormulaOne.Shared;

namespace FormulaOne.Application.Services.Abstractions.WriteInterfaces
{
    public interface IWriteCarService
    {
        Task<bool> AddCar(CarRequestDto carRequest);
        Task DeleteCar(Guid CarId,string TeamName,string year);
    }
}
