using FormulaOne.Application.Dto.Response;
using FormulaOne.Application.RepoAbstractions.CircuitRepo;
using FormulaOne.Application.Services.Abstractions.ReadInterfaces;
using FormulaOne.Infrastructure.Repositories.Read;
using FormulaOne.Infrastructure.Storage.Redis;
using FormulaOne.Shared;

namespace FormulaOne.Application.Services.ReadServices
{
    public class ReadCircuitService : IReadCircuitService
    {
        private readonly ICircuitReadRepository _circuitRead;
        private readonly IRedisCache _cache;
        public ReadCircuitService(ICircuitReadRepository circuitRead, IRedisCache cache)
        {
            _circuitRead = circuitRead;
            _cache = cache;
        }

        public async Task<Result<IEnumerable<CircuitResponseDto>>> GetAllCircuits()
        {
            var cache = await _cache.Get<CircuitResponseDto>("circuit:all");
            if (cache!.Count != 0)
            {
                return Result<IEnumerable<CircuitResponseDto>>.Success(cache);
            }
            var circuits = await _circuitRead.GetAllCircuit();
            var response = circuits.Select(c => new CircuitResponseDto(c.Title, c.CountryLocation, c.Length)).ToList();
            if (response.Count != 0)
            {
                await _cache.Set("circuit:all", response, TimeSpan.FromDays(1));
                return Result<IEnumerable<CircuitResponseDto>>.Success(response);
            }
            else
            {
                return Result<IEnumerable<CircuitResponseDto>>.Error("Список трасс пуст!");
            }
        }

        public async Task<Result<CircuitResponseDto>> GetById(Guid Id)
        {
            var cache = await _cache.Get<CircuitResponseDto>($"circuit:{Id}");
            if(cache!.Count != 0)
            {
                return Result<CircuitResponseDto>.Success(cache.Single());
            }
            var circuit = await _circuitRead.GetCircuitById(Id);
            if (circuit != null) 
            {
                await _cache.Set($"circuit:{Id}",circuit);
                return Result<CircuitResponseDto>
                .Success(new CircuitResponseDto(circuit.Title, circuit.CountryLocation, circuit.Length));
            }
            else
            {
                return Result<CircuitResponseDto>.Error("Трасса не найдена");
            } 
                
        }
    }
}
