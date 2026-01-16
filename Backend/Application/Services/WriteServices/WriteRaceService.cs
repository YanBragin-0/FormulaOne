using FormulaOne.Application.Dto.Request;
using FormulaOne.Application.RepoAbstractions.RaceRepo;
using FormulaOne.Application.Services.Abstractions.WriteInterfaces;
using FormulaOne.Entities;

namespace FormulaOne.Application.Services.WriteServices
{
    public class WriteRaceService(IRaceWriteRepository repository) : IWriteRaceService
    {
        private readonly IRaceWriteRepository _repository = repository;
        public async Task AddRace(RaceRequestDto requestDto)
        {
            var race = new Race(
                requestDto.CircuitId,
                requestDto.Title,
                requestDto.DateTime
            );
            await _repository.AddRace(race);
        }

        public async Task DeleteRace(Guid Id) => await _repository.DeleteByIdRace(Id); 

    }
}
