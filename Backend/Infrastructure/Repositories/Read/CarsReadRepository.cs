using FormulaOne.Application.RepoAbstractions.CarsRepo;
using FormulaOne.Entities;
using FormulaOne.Infrastructure.Storage;
using Microsoft.EntityFrameworkCore;

namespace FormulaOne.Infrastructure.Repositories.Read
{
    public class CarsReadRepository : ICarsReadRepository
    {
        private readonly ApplicationDbContext _context;
        public CarsReadRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Cars>> GetAllCars()
        {
            var result = await _context.Cars.ToListAsync();
            return result.Count() != 0 ? result : Enumerable.Empty<Cars>();
        }

        public async Task<Cars> GetTeamCars(Teams team,Season season)
        {
            var result = await _context.Cars.Where(c => c.TeamId == team.Id && c.SeasonId == season.Id).FirstOrDefaultAsync();
            return result!;
        }
    }
}
