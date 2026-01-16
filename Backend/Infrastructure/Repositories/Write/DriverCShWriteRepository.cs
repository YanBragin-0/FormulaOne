using FormulaOne.Application.RepoAbstractions.DriverCShRepo;
using FormulaOne.Entities;
using FormulaOne.Infrastructure.Storage;

namespace FormulaOne.Infrastructure.Repositories.Write
{
    public class DriverCShWriteRepository : IDriverCShWriteRepository
    {
        private readonly ApplicationDbContext _context;
        public DriverCShWriteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddDriverToTable(DriverChampionship driver)
        {
            await _context.DriverChampionship.AddAsync(driver);
            await _context.SaveChangesAsync();
        }
    }
}
