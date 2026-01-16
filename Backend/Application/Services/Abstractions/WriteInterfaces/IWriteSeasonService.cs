using FormulaOne.Application.Dto.Request;

namespace FormulaOne.Application.Services.Abstractions.WriteInterfaces
{
    public interface IWriteSeasonService
    {
        Task AddSeason(SeasonRequestDto requestDto);
        Task DeleteSeason(Guid Id);
    }
}
