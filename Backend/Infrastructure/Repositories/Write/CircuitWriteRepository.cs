using FormulaOne.Application.RepoAbstractions.CircuitRepo;
using FormulaOne.Entities;
using FormulaOne.Infrastructure.Storage;
using Microsoft.EntityFrameworkCore;

namespace FormulaOne.Infrastructure.Repositories.Write
{
    public class CircuitWriteRepository : ICircuitWriteRepository
    {
        private readonly ApplicationDbContext _context;
        public CircuitWriteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddCircuit(RaceCircuit circuit)
        {
            var isvalid = await _context.RaceCircuits.FirstOrDefaultAsync(c =>c.Title.ToLower() == circuit.Title.ToLower());
            if (isvalid != null) { return false; }
            await _context.RaceCircuits.AddAsync(circuit);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task DeleteCircuitById(Guid Id)
        {
            var circuit = await _context.RaceCircuits.FindAsync(Id);
            if (circuit is not null)
            {
                _context.RaceCircuits.Remove(circuit);
                await _context.SaveChangesAsync();
            }
        }
    }
}
