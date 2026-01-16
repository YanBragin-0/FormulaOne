using FormulaOne.Application.RepoAbstractions.TeamsRepo;
using FormulaOne.Entities;
using FormulaOne.Infrastructure.Storage;
using Microsoft.EntityFrameworkCore;

namespace FormulaOne.Infrastructure.Repositories.Read
{
    public class TeamsReadRepository : ITeamReadRepository
    {
        private readonly ApplicationDbContext _context;
        public TeamsReadRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Teams>> GetAllTeams()
        {
            var result = await _context.Teams.ToListAsync();
            return result.Count() != 0 ? result : Enumerable.Empty<Teams>();
        }

        public async Task<Teams> GetTeamById(Guid TeamId) => (await _context.Teams.FirstOrDefaultAsync(t => t.Id == TeamId))!;

        public async Task<Teams> GetTeamByName(string Name)
        {
            Name = Name.ToLower();
            var result = await _context.Teams.FirstOrDefaultAsync(t => t.TeamName.ToLower() == Name);
            return result!; 
        }
    }
}
