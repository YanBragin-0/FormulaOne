using FormulaOne.Application.Dto.Request;
using FormulaOne.Application.RepoAbstractions.SeasonRepo;
using FormulaOne.Application.Services.Abstractions.WriteInterfaces;
using FormulaOne.Entities;

namespace FormulaOne.Application.Services.WriteServices
{
    public class WriteSeasonService(ISeasonWriteRepository repository) : IWriteSeasonService
    {
        private readonly ISeasonWriteRepository _repository = repository;
        public async Task AddSeason(SeasonRequestDto requestDto)
        {
            var season = new Season(requestDto.year);
            await _repository.AddSeason(season);
        }

        public async Task DeleteSeason(Guid SeasonId) => await _repository.DeleteSeason(SeasonId);

    }
}
