using FormulaOne.Application.Dto.Request;
using FormulaOne.Application.RepoAbstractions.DriversRepo;
using FormulaOne.Application.Services.Abstractions.WriteInterfaces;
using FormulaOne.Entities;

namespace FormulaOne.Application.Services.WriteServices
{
    public class WriteDriverService(IDriverWriteRepository repository) : IWriteDriverService
    {
        private readonly IDriverWriteRepository _repository = repository;
        public async Task AddDriver(DriverRequestDto requestDto)
        {
            var driver = new Drivers(
                requestDto.TeamId,
                requestDto.Name,
                requestDto.Age,
                requestDto.Country,
                requestDto.Biography
            );
            await _repository.AddDriver(driver);
        }

        public async Task DeleteDriver(Guid DriverId) => await _repository.DeleteDriverById(DriverId);

    }
}
