using FormulaOne.Application.RepoAbstractions.DriverCShRepo;
using FormulaOne.Entities;
using FormulaOne.Infrastructure.Storage;
using Microsoft.EntityFrameworkCore;

namespace FormulaOne.Infrastructure.Repositories.Read
{
    public class DriverCShReadRepository : IDriverCShReadRepository
    {
        private readonly ApplicationDbContext _context;
        public DriverCShReadRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DriverChampionship>> GetDriversTableBySeason(Season season)
        {
            var result = await _context.DriverChampionship.Where(cs => cs.SeasonId == season.Id).ToListAsync();
            return result.Count() != 0 ? result : Enumerable.Empty<DriverChampionship>();
        }
    }
}
