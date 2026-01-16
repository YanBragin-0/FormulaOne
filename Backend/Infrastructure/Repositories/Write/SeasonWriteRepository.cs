using FormulaOne.Application.RepoAbstractions.SeasonRepo;
using FormulaOne.Entities;
using FormulaOne.Infrastructure.Storage;

namespace FormulaOne.Infrastructure.Repositories.Write
{
    public class SeasonWriteRepository : ISeasonWriteRepository 
    {
        private readonly ApplicationDbContext _context;
        public SeasonWriteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddSeason(Season season)
        {
            await _context.Seasons.AddAsync(season);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSeason(Guid seasonId)
        {
            var s = await _context.Seasons.FindAsync(seasonId);
            if (s is not null){ _context.Seasons.Remove(s); }
            await _context.SaveChangesAsync();
        }
    }
}
