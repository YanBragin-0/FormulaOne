using FormulaOne.Application.RepoAbstractions.SeasonRepo;
using FormulaOne.Entities;
using FormulaOne.Infrastructure.Storage;
using Microsoft.EntityFrameworkCore;

namespace FormulaOne.Infrastructure.Repositories.Read
{
    public class SeasonReadRepository : ISeasonReadRepository
    {
        private readonly ApplicationDbContext _context;
        public SeasonReadRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Season> GetSeasonByYear(short Year)
        {
            return (await _context.Seasons.FirstOrDefaultAsync(s => s.Year == Year))!;
        }
    }
}
