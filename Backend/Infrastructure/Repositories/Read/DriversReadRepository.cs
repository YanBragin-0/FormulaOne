using FormulaOne.Application.RepoAbstractions.DriversRepo;
using FormulaOne.Entities;
using FormulaOne.Infrastructure.Storage;
using Microsoft.EntityFrameworkCore;

namespace FormulaOne.Infrastructure.Repositories.Read
{
    public class DriversReadRepository : IDriverReadRepository
    {
        private readonly ApplicationDbContext _context;
        public DriversReadRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Drivers>> GetAllDrivers()
        {
            var result = await _context.Drivers.ToListAsync();
            return result.Count() != 0 ? result : Enumerable.Empty<Drivers>();
        }

        public async Task<Drivers> GetDriverById(Guid DriverId)
        {
            var result = await _context.Drivers.FirstOrDefaultAsync(d => d.Id == DriverId);
            return result;
        }
        public async Task<IEnumerable<Drivers>> GetTeamDrivers(Teams Team)
        {
            var result = await _context.Drivers.Where(d => d.TeamId == Team.Id).ToListAsync();
            return result.Count() != 0 ? result : Enumerable.Empty<Drivers>();
        }
    }
}
