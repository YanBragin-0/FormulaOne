using FormulaOne.Application.RepoAbstractions.TeamsRepo;
using FormulaOne.Entities;
using FormulaOne.Infrastructure.Storage;

namespace FormulaOne.Infrastructure.Repositories.Write
{
    public class TeamsWriteRepository : ITeamWriteRepository
    {
        private readonly ApplicationDbContext _context;
        public TeamsWriteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddTeam(Teams Team)
        {
            await _context.Teams.AddAsync(Team);
            await _context.SaveChangesAsync();  
        }

        public void AddTeamBiography(Teams team, string Biography)
        {
            team.AddBiography(Biography);
        }

        public async Task DeleteTeamById(Guid Id)
        {
            var team = await _context.Teams.FindAsync(Id);
            if (team is not null) { _context.Teams.Remove(team); }
            await _context.SaveChangesAsync();
        }

        public async Task<Teams> GetById(Guid Id) => await _context.Teams.FindAsync(Id);
    }
}
