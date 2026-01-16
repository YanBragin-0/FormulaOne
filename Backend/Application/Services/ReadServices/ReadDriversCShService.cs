using FormulaOne.Application.Dto.Response;
using FormulaOne.Application.RepoAbstractions.DriverCShRepo;
using FormulaOne.Application.RepoAbstractions.SeasonRepo;
using FormulaOne.Application.Services.Abstractions.ReadInterfaces;
using FormulaOne.Infrastructure.Storage.Redis;
using FormulaOne.Shared;

namespace FormulaOne.Application.Services.ReadServices
{
    public class ReadDriversCShService : IReadDriverCShService
    {
        private readonly IDriverCShReadRepository _driverCShRead;
        private readonly ISeasonReadRepository _seasonRead;
        private readonly IRedisCache _cache;
        public ReadDriversCShService(IDriverCShReadRepository driver,
                                    ISeasonReadRepository season,
                                    IRedisCache cache)
        {
            _driverCShRead = driver;
            _seasonRead = season;
            _cache = cache;
        }
        public async Task<Result<IEnumerable<DriverTableResponse>>> GetDriversTable(short year)
        {
            var cache = await _cache.Get<IEnumerable<DriverTableResponse>>($"drivertable:{year}");
            if(cache!.Count != 0)
            {
                return Result<IEnumerable<DriverTableResponse>>.Success(cache.Single());
            }
            var season = await _seasonRead.GetSeasonByYear(year);
            var table = await _driverCShRead.GetDriversTableBySeason(season);
            var result = table.Select(t => new DriverTableResponse(t.DriverName,t.TeamName,t.Points)).OrderByDescending(t => t.Points).ToList();
            if(result.Count != 0)
            {
                await _cache.Set($"drivertable:{year}",result);
                return Result<IEnumerable<DriverTableResponse>>.Success(result);
            }
            else
            {
                return Result<IEnumerable<DriverTableResponse>>.Error("Таблица пуста");
            }

                                            
        }
    }
}
