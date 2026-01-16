using FormulaOne.Application.Dto.Response;
using FormulaOne.Application.RepoAbstractions.TeamsRepo;
using FormulaOne.Application.Services.Abstractions.ReadInterfaces;
using FormulaOne.Infrastructure.Storage.Redis;
using FormulaOne.Shared;

namespace FormulaOne.Application.Services.ReadServices
{
    public class ReadTeamService : IReadTeamService
    {
        private readonly ITeamReadRepository _teamRead;
        private readonly IRedisCache _cache;
        public ReadTeamService(ITeamReadRepository teamRead, IRedisCache cache)
        {
            _teamRead = teamRead;
            _cache = cache;
        }
        public async Task<Result<IEnumerable<TeamResponseDto>>> GetAllTeams()
        {
            var cache = await _cache.Get<TeamResponseDto>("team:all");
            if (cache!.Count != 0)
            {
                return Result<IEnumerable<TeamResponseDto>>.Success(cache);
            }
            var result = await _teamRead.GetAllTeams();
            if (result.Any())
            {
                var forCache = result.Select(t => new TeamResponseDto(t.TeamName, t.Biography!)).ToList();
                await _cache.Set("team:all", forCache, TimeSpan.FromDays(1));
                return Result<IEnumerable<TeamResponseDto>>.Success(forCache);
            }
            else
            {
                return Result<IEnumerable<TeamResponseDto>>.Error("Список команд пуст!");
            }
        }

        public async Task<Result<TeamResponseDto>> GetTeamByName(string TeamName)
        {
            var cache = await _cache.Get<TeamResponseDto>($"team:{TeamName}");
            if (cache!.Count != 0)
            {
                return Result<TeamResponseDto>.Success(cache.Single());
            }
            var team = await _teamRead.GetTeamByName(TeamName);
            if (team != null)
            {
                var forCache = new TeamResponseDto(team.TeamName, team.Biography!);
                await _cache.Set($"team:{TeamName}", forCache, TimeSpan.FromDays(1));
                return Result<TeamResponseDto>.Success(forCache);
            }
            else
            {
                return Result<TeamResponseDto>.Error("Команда не найдена");
            }
                 
        }
    }
}
