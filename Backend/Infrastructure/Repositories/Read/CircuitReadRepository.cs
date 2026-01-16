using FormulaOne.Application.RepoAbstractions.CircuitRepo;
using FormulaOne.Entities;
using FormulaOne.Infrastructure.Storage;
using Microsoft.EntityFrameworkCore;

namespace FormulaOne.Infrastructure.Repositories.Read
{
    public class CircuitReadRepository : ICircuitReadRepository
    {
        private readonly ApplicationDbContext _context;
        public CircuitReadRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RaceCircuit>> GetAllCircuit()
        {
            var result = await _context.RaceCircuits.ToListAsync();
            return result.Count() != 0 ? result : Enumerable.Empty<RaceCircuit>();
        }

        public async Task<RaceCircuit> GetByCountry(string Country = "") => await 
            _context.RaceCircuits.FirstOrDefaultAsync(c => c.CountryLocation.ToLower() == Country.ToLower());

        public async Task<RaceCircuit> GetCircuitById(Guid CircuitId)
        {
            return (await _context.RaceCircuits.FirstOrDefaultAsync(c => c.Id == CircuitId))!;
        }
    }
}
