using FormulaOne.Application.RepoAbstractions.RaceRepo;
using FormulaOne.Entities;
using FormulaOne.Infrastructure.Storage;
using Microsoft.EntityFrameworkCore;

namespace FormulaOne.Infrastructure.Repositories.Read
{
    public class RaceReadRepository : IRaceReadRepository
    {
        private readonly ApplicationDbContext _context;
        public RaceReadRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Race>> GetAllRaces()
        {
            var result = await _context.Races.ToListAsync();
            return result.Count() != 0 ? result : Enumerable.Empty<Race>();
        }

        public async Task<Race> GetRaceById(Guid Id)
        {
            return (await _context.Races.FirstOrDefaultAsync(r => r.Id == Id))!; 
        }
    }
}
