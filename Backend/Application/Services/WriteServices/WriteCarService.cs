using FormulaOne.Application.Dto.Request;
using FormulaOne.Application.RepoAbstractions.CarsRepo;
using FormulaOne.Application.Services.Abstractions.WriteInterfaces;
using FormulaOne.Entities;
using FormulaOne.Infrastructure.Storage.Redis;
using FormulaOne.Shared;

namespace FormulaOne.Application.Services.WriteServices
{
    public class WriteCarService(ICarsWriteRepository repository,IRedisCache cache) : IWriteCarService
    {
        private readonly ICarsWriteRepository _repository = repository;
        private readonly IRedisCache _cache = cache;

        public async Task<bool> AddCar(CarRequestDto carRequest)
        {
            bool IsValid = await _repository.CheckSeasonValidCount(carRequest.SeasonId,carRequest.TeamId);
            if (!IsValid)
            {
                return false;
            }
            var car = new Cars(carRequest.TeamId, carRequest.SeasonId,carRequest.Title, carRequest.Description);
            await _cache.RemoveByPrefixAsync("car:all");
            await _repository.AddCar(car);
            return true;
        }

        public async Task DeleteCar(Guid CarId, string TeamName, string year)
        {
            await _cache.RemoveByPrefixAsync("car:all");
            await _cache.RemoveByPrefixAsync($"car:{TeamName}{year}");
            await _repository.DeleteById(CarId);
        }
    }
}
