using FormulaOne.Application.Dto.Request;
using FormulaOne.Application.RepoAbstractions.CircuitRepo;
using FormulaOne.Application.Services.Abstractions.WriteInterfaces;
using FormulaOne.Entities;
using FormulaOne.Infrastructure.Storage.Redis;
using FormulaOne.Shared;

namespace FormulaOne.Application.Services.WriteServices
{
    public class WriteCircuitService(ICircuitWriteRepository repository,IRedisCache cache) 
        : IWriteCircuitService
    {
        private readonly ICircuitWriteRepository _repository = repository;
        private readonly IRedisCache _cache = cache;
        public async Task<Result<bool>> AddCircuit(CircuitRequestDto circuitRequest)
        {
            var circuit = new RaceCircuit(circuitRequest.Title, circuitRequest.Country, circuitRequest.Length);
            var result = await _repository.AddCircuit(circuit);
            if(result == true)
            {
                await _cache.RemoveByPrefixAsync($"circuit:all");
                return Result<bool>.Success(result);
            }
            else
            {
                return Result<bool>.Error("Такая трасса уже существует");
            }
            
        }

        public async Task RemoveCircuit(Guid Id)
        {
            await _cache.RemoveByPrefixAsync($"circuit:{Id}");
            await _cache.RemoveByPrefixAsync("circuit:all");
            await _repository.DeleteCircuitById(Id);
        }
    }
}
