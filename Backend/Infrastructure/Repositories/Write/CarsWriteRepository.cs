using FormulaOne.Application.RepoAbstractions.CarsRepo;
using FormulaOne.Entities;
using FormulaOne.Infrastructure.Storage;
using Microsoft.EntityFrameworkCore;

namespace FormulaOne.Infrastructure.Repositories.Write
{
    public class CarsWriteRepository : ICarsWriteRepository
    {
        private readonly ApplicationDbContext _context;
        public CarsWriteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddCar(Cars car)
        {
            await _context.Cars.AddAsync(car);
            await _context.SaveChangesAsync();  
        }

        public async Task<bool> CheckSeasonValidCount(Guid SeasonId,Guid TeamId)
        {
            var isvalid = await _context.Cars
                .Where(c => c.SeasonId == SeasonId && c.TeamId == TeamId)
                .FirstOrDefaultAsync();
            return isvalid is not null ? false : true;
        }

        public async Task DeleteById(Guid Id)
        {
            var car = await _context.Cars.FindAsync(Id);
            if (car is not null)
            {
                _context.Remove(car);
                await _context.SaveChangesAsync();
            }
        }
    }
}
