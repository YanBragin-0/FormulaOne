using FormulaOne.Application.Dto.Request;
using FormulaOne.Application.Dto.Response;
using FormulaOne.Application.RepoAbstractions.CarsRepo;
using FormulaOne.Application.RepoAbstractions.SeasonRepo;
using FormulaOne.Application.RepoAbstractions.TeamsRepo;
using FormulaOne.Application.Services.Abstractions.ReadInterfaces;
using FormulaOne.Entities;
using FormulaOne.Infrastructure.Storage.Redis;
using FormulaOne.Shared;

namespace FormulaOne.Application.Services.ReadServices
{
    public class ReadCarService : IReadCarService
    {
        private readonly ICarsReadRepository _carsRead;
        private readonly ITeamReadRepository _teamRead;
        private readonly ISeasonReadRepository _seasonRead;
        private readonly IRedisCache _cache;
        public ReadCarService(ICarsReadRepository carsRead,ITeamReadRepository teamRead,IRedisCache cache,ISeasonReadRepository seasonRead)
        {
            _carsRead = carsRead;
            _teamRead = teamRead;
            _cache = cache;
            _seasonRead = seasonRead;
        }

        public async Task<Result<IEnumerable<CarsResponseDto>>> GetAllCars()
        {
            var cache = await _cache.Get<CarsResponseDto>("car:all");
            if (cache!.Count != 0)
            {
                return Result<IEnumerable<CarsResponseDto>>.Success(cache);
            }
            var teams = await _teamRead.GetAllTeams();
            var cars = await _carsRead.GetAllCars();
            var response = (from c in cars
                           join t in teams
                                on c.TeamId equals t.Id
                           select new CarsResponseDto(c.Title,c.Description,t.TeamName)).ToList();
            if (response.Count != 0)
            {
                await _cache.Set<List<CarsResponseDto>>("car:all", response, TimeSpan.FromMinutes(20));
                return Result<IEnumerable<CarsResponseDto>>.Success(response);
            }
            else
            {
                return Result<IEnumerable<CarsResponseDto>>.Error("Список болидов пока пуст");
            }
        }

        public async Task<Result<CarsResponseDto>> GetTeamCar(TeamRequestDto Team, SeasonRequestDto season)
        {
            var cache = await _cache.Get<CarsResponseDto>($"car:{Team.TeamName}{season.year}");
            if (cache!.Count != 0)
            {
                return Result<CarsResponseDto>.Success(cache.Single());
            }
            var team = await _teamRead.GetTeamByName(Team.TeamName);
            if (team == null) return Result<CarsResponseDto>.Error("Команда не найдена!");
            var _season = await _seasonRead.GetSeasonByYear(season.year);
            var car = await _carsRead.GetTeamCars(team,_season);
            if (car == null) return Result<CarsResponseDto>.Error("Болид не найден");
            var response = new CarsResponseDto(car.Title,car.Description,team.TeamName);
            await _cache.Set($"car:{Team.TeamName}{season.year}", response, TimeSpan.FromDays(1));
            return Result<CarsResponseDto>.Success(response);
            
        }
    }
}
