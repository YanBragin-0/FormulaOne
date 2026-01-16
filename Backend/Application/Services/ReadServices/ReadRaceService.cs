using FormulaOne.Application.Dto.Response;
using FormulaOne.Application.RepoAbstractions.CircuitRepo;
using FormulaOne.Application.RepoAbstractions.RaceRepo;
using FormulaOne.Application.Services.Abstractions.ReadInterfaces;
using FormulaOne.Infrastructure.Storage.Redis;
using FormulaOne.Shared;

namespace FormulaOne.Application.Services.ReadServices
{
    public class ReadRaceService : IReadRaceService
    {
        private readonly IRaceReadRepository _raceRead;
        private readonly ICircuitReadRepository _circuitRead;
        private readonly IRedisCache _cache;
        public ReadRaceService(IRaceReadRepository raceRead,
                                ICircuitReadRepository circuitRead,
                                IRedisCache cache)
        {
            _raceRead = raceRead;
            _circuitRead = circuitRead;
            _cache = cache;
        }
        public async Task<Result<IEnumerable<RaceResponseDto>>> GetAllRaces()
        {
            var cache = await _cache.Get<RaceResponseDto>("race:all");
            if(cache!.Count != 0)
            {
                return Result<IEnumerable<RaceResponseDto>>.Success(cache);
            }
            var races = await _raceRead.GetAllRaces();
            var circuits = await _circuitRead.GetAllCircuit();
            var result = (from r in races
                         join c in circuits
                            on r.CircuitId equals c.Id
                         select new RaceResponseDto(r.DateTime!, c.Title, c.CountryLocation, c.Length)).OrderBy(r => r.DateTime).ToList();
            if(result.Count !=0)
            {
                await _cache.Set("race:all", result, TimeSpan.FromDays(1));
                return Result<IEnumerable<RaceResponseDto>>.Success(result);
            }
            else
            {
                return Result<IEnumerable<RaceResponseDto>>.Error("Список гонок пуст");
            }
                                  
        }

        public async Task<Result<RaceResponseDto>> GetRaceByCountry(string Country)
        {
            var circuit = await _circuitRead.GetByCountry(Country);
            if (circuit is null)
            {
                return Result<RaceResponseDto>.Error("Гонка не найдена");
            }
            var races = await _raceRead.GetAllRaces();
            var result = races.FirstOrDefault(r => r.CircuitId == circuit.Id);
            return result is not null ? Result<RaceResponseDto>
                       .Success(new RaceResponseDto(result.DateTime, circuit.Title, circuit.CountryLocation, circuit.Length))
                       : Result<RaceResponseDto>.Error("Гонка не найдена!");
        }
    }
}
