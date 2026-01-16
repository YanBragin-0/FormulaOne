using FormulaOne.Application.RepoAbstractions.ConstructorsCShRepo;
using FormulaOne.Entities;
using FormulaOne.Infrastructure.Storage;

namespace FormulaOne.Infrastructure.Repositories.Write
{
    public class ConstructorsWriteRepository : IConstructorsCShWriteRepository
    {
        private readonly ApplicationDbContext _context;
        public ConstructorsWriteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddTeamToTable(ConstructorsChampionship team)
        {
            await _context.ConstructorsChampionship.AddAsync(team);
            await _context.SaveChangesAsync();
        }
    }
}
