using FormulaOne.Application.RepoAbstractions.RaceRepo;
using FormulaOne.Entities;
using FormulaOne.Infrastructure.Storage;

namespace FormulaOne.Infrastructure.Repositories.Write
{
    public class RaceWriteRepository : IRaceWriteRepository
    {
        private readonly ApplicationDbContext _context;
        public RaceWriteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddRace(Race race)
        {
            await _context.Races.AddAsync(race);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIdRace(Guid Id)
        {
            var race = await _context.Races.FindAsync(Id);
            if (race is not null) { _context.Races.Remove(race); }
            await _context.SaveChangesAsync();
        }
    }
}
