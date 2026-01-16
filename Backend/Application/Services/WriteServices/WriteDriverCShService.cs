using FormulaOne.Application.Dto.Request;
using FormulaOne.Application.RepoAbstractions.DriverCShRepo;
using FormulaOne.Application.Services.Abstractions.WriteInterfaces;
using FormulaOne.Entities;
using FormulaOne.Infrastructure.Storage.Redis;

namespace FormulaOne.Application.Services.WriteServices
{
    public class WriteDriverCShService(IDriverCShWriteRepository repository,
                                       IRedisCache cache) : IWriteDriverCShService
    {
        private readonly IDriverCShWriteRepository _repository = repository;
        private readonly IRedisCache _cache = cache;
        public async Task AddDriverCShRecord(DriverCShRequestDto requestDto)
        {
            var tableRecord = new DriverChampionship(
                requestDto.SeasonId,
                requestDto.DriverId,
                requestDto.DriverName,
                requestDto.TeamName,
                requestDto.Points
            );
            await _cache.RemoveByPrefixAsync($"drivertable:{requestDto.year}");
            await _repository.AddDriverToTable(tableRecord);

        }
    }
}
