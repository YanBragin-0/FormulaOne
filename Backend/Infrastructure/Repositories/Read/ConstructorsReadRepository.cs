using FormulaOne.Application.RepoAbstractions.ConstructorsCShRepo;
using FormulaOne.Entities;
using FormulaOne.Infrastructure.Storage;
using Microsoft.EntityFrameworkCore;

namespace FormulaOne.Infrastructure.Repositories.Read
{
    public class ConstructorsReadRepository : IConstructorsCShReadRepository
    {
        private readonly ApplicationDbContext _context;
        public ConstructorsReadRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ConstructorsChampionship>> GetTableBySeason(Season season)
        {
            var result = await _context.ConstructorsChampionship.Where(cs => cs.SeasonId == season.Id).ToListAsync();
            return result.Count() != 0 ? result : Enumerable.Empty<ConstructorsChampionship>();
        }
    }
}
