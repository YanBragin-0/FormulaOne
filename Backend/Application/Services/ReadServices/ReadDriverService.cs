using FormulaOne.Application.Dto.Response;
using FormulaOne.Application.RepoAbstractions.DriversRepo;
using FormulaOne.Application.RepoAbstractions.TeamsRepo;
using FormulaOne.Application.Services.Abstractions.ReadInterfaces;
using FormulaOne.Infrastructure.Storage.Redis;
using FormulaOne.Shared;

namespace FormulaOne.Application.Services.ReadServices
{
    public class ReadDriverService : IReadDriverService
    {
        private readonly IDriverReadRepository _driverRead;
        private readonly ITeamReadRepository _teamRead;
        private readonly IRedisCache _cache;
        public ReadDriverService(IDriverReadRepository driverRead,
                                    ITeamReadRepository teamRead,
                                    IRedisCache cache)
        {
            _driverRead = driverRead;
            _teamRead = teamRead;
            _cache = cache;
        }
        public async Task<Result<IEnumerable<DriverResponseDto>>> GetAllDrivers()
        {
            var cache = await _cache.Get<IEnumerable<DriverResponseDto>>("driver:all");
            if(cache!.Count != 0)
            {
                return Result<IEnumerable<DriverResponseDto>>.Success(cache.Single());
            }
            var teams = await _teamRead.GetAllTeams();
            var drivers = await _driverRead.GetAllDrivers();
            var result = (from d in drivers
                         join t in teams
                            on d.TeamId equals t.Id
                         select new DriverResponseDto(d.Name,d.Age,d.Country,t.TeamName,d.Biography!)).OrderBy(d => d.Team).ToList();
            if (result.Count != 0) 
            {
                await _cache.Set("driver:all",result);
                return Result<IEnumerable<DriverResponseDto>>.Success(result);
            }
            else
            {
                return Result<IEnumerable<DriverResponseDto>>.Error("Таблица пилотов пуста!");
            }
 
        }

        public async Task<Result<IEnumerable<DriverResponseDto>>> GetTeamDrivers(string TeamName)
        {
            var cache = await _cache.Get<DriverResponseDto>($"driver:{TeamName}");
            if(cache!.Count != 0)
            {
                return Result<IEnumerable<DriverResponseDto>>.Success(cache);
            }
            var team = await _teamRead.GetTeamByName(TeamName);
            if(team == null)
            {
                return Result<IEnumerable<DriverResponseDto>>.Error("Список пилотов пуст или введите корректное имя");
            }
            var drivers = await _driverRead.GetTeamDrivers(team);
            if (drivers.Any())
            {
                var result = drivers.Select(d => new DriverResponseDto(d.Name, d.Age, d.Country, team.TeamName, d.Biography!)).ToList();
                await _cache.Set($"driver:{TeamName}",result);
                return Result<IEnumerable<DriverResponseDto>>.Success(result);
            }
            else
            {
                return Result<IEnumerable<DriverResponseDto>>.Error("Список пилотов пуст");
            }
            
        }
    }
}
