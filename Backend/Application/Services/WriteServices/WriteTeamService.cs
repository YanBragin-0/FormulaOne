using FormulaOne.Application.Dto.Request;
using FormulaOne.Application.RepoAbstractions.TeamsRepo;
using FormulaOne.Application.Services.Abstractions.WriteInterfaces;
using FormulaOne.Entities;
using FormulaOne.Infrastructure.Storage.Redis;

namespace FormulaOne.Application.Services.WriteServices
{
    public class WriteTeamService(ITeamWriteRepository repository,IRedisCache cache) : IWriteTeamService
    {
        private readonly ITeamWriteRepository _repository = repository;
        private readonly IRedisCache _cache = cache;
        public async Task AddBiography(Guid TeamId,TeamRequestDto requestDto)
        {
            await _cache.RemoveByPrefixAsync($"team:{requestDto.TeamName}");
            var Team = await _repository.GetById(TeamId);
            Team.AddBiography(requestDto.Biography ?? "");
        }

        public async Task AddTeam(TeamRequestDto requestDto)
        {
            await _cache.RemoveByPrefixAsync($"team:{requestDto.TeamName}");
            var team = new Teams(requestDto.TeamName,requestDto.Biography);
            await _repository.AddTeam(team);
        }

        public async Task DeleteTeam(Guid TeamId)
        {
            await _cache.RemoveAllByPrefixAsync("team");
            await _repository.DeleteTeamById(TeamId);
        }
    }
}
