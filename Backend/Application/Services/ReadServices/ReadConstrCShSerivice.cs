using FormulaOne.Application.Dto.Response;
using FormulaOne.Application.RepoAbstractions.ConstructorsCShRepo;
using FormulaOne.Application.RepoAbstractions.SeasonRepo;
using FormulaOne.Application.Services.Abstractions.ReadInterfaces;
using FormulaOne.Infrastructure.Storage.Redis;
using FormulaOne.Shared;

namespace FormulaOne.Application.Services.ReadServices
{
    public class ReadConstrCShSerivice : IReadConstrCShService
    {
        private readonly IConstructorsCShReadRepository _constrreadRepository;
        private readonly ISeasonReadRepository _seasonReadRepository;
        private readonly IRedisCache _cache;
        public ReadConstrCShSerivice(IConstructorsCShReadRepository repository,
                                        ISeasonReadRepository seasonRead,
                                        IRedisCache cache)
        {
            _constrreadRepository = repository;
            _seasonReadRepository = seasonRead;
            _cache = cache;
        }
        public async Task<Result<IEnumerable<TeamTableResponseDto>>> GetTeableBySeason(short year)
        {
            var cache = await _cache.Get<TeamTableResponseDto>($"teamtable:{year}");
            if(cache!.Count != 0)
            {
                return Result<IEnumerable<TeamTableResponseDto>>.Success(cache);
            }
            var season = await _seasonReadRepository.GetSeasonByYear(year);
            var table = await _constrreadRepository.GetTableBySeason(season);
            var result = table.Select(t => new TeamTableResponseDto(t.TeamName,t.Points)).OrderByDescending(t => t.Points).ToList();
            if (result.Count != 0)
            {
                await _cache.Set($"teamtable:{year}", result, TimeSpan.FromDays(1));
                return Result<IEnumerable<TeamTableResponseDto>>.Success(result);
            }
            else 
            { 
                return Result<IEnumerable<TeamTableResponseDto>>.Error("Таблица пуста");
            }
                                            
        }
    }
}
