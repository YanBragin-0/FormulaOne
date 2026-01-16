using FormulaOne.Application.Dto.Request;
using FormulaOne.Application.RepoAbstractions.ConstructorsCShRepo;
using FormulaOne.Application.Services.Abstractions.WriteInterfaces;
using FormulaOne.Entities;
using FormulaOne.Infrastructure.Storage.Redis;

namespace FormulaOne.Application.Services.WriteServices
{
    public class WriteConstrCshSrevice(IConstructorsCShWriteRepository writeRepository,
                                       IRedisCache cache) : IWriteConstrCShService
    {
        private readonly IConstructorsCShWriteRepository _writeRepository = writeRepository;
        private readonly IRedisCache _cache = cache;
        public async Task AddConstrCShRecord(ConstrCShRequestDto requestDto)
        {
            var tableRecord = new ConstructorsChampionship(
                requestDto.SeasonId,
                requestDto.TeamId,
                requestDto.TeamName,
                requestDto.Points
            );
            await _cache.RemoveByPrefixAsync($"teamtable:{requestDto.year}");
            await _writeRepository.AddTeamToTable(tableRecord); 
        }
    }
}
