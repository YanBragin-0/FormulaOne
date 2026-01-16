using FormulaOne.Application.RepoAbstractions.DriversRepo;
using FormulaOne.Entities;
using FormulaOne.Infrastructure.Storage;

namespace FormulaOne.Infrastructure.Repositories.Write
{
    public class DriversWriteRepository : IDriverWriteRepository
    {
        private readonly ApplicationDbContext _context;
        public DriversWriteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddDriver(Drivers Driver)
        {
            await _context.Drivers.AddAsync(Driver);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteDriverById(Guid Id)
        {
            var driver = await _context.Drivers.FindAsync(Id);
            if (driver is not null) { _context.Drivers.Remove(driver); }
            await _context.SaveChangesAsync();

        }
    }
}
