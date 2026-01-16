using FormulaOne.Application.Dto.Request;

namespace FormulaOne.Application.Services.Abstractions.WriteInterfaces
{
    public interface IWriteRaceService
    {
        Task AddRace(RaceRequestDto requestDto);
        Task DeleteRace(Guid Id);
    }

}
