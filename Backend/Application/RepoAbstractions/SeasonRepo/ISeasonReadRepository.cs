using FormulaOne.Entities;

namespace FormulaOne.Application.RepoAbstractions.SeasonRepo
{
    public interface ISeasonReadRepository
    {
        Task<Season> GetSeasonByYear(short Year);

    }
}
