using FormulaOne.Application.Dto.Request;

namespace FormulaOne.Application.Services.Abstractions.WriteInterfaces
{
    public interface IWriteConstrCShService
    {
        Task AddConstrCShRecord(ConstrCShRequestDto requestDto);
    }
}
