using FormulaOne.Entities;

namespace FormulaOne.Application.RepoAbstractions.SeasonRepo
{
    public interface ISeasonWriteRepository
    {
        Task AddSeason(Season season);
        Task DeleteSeason(Guid seasonId);
    }
}
