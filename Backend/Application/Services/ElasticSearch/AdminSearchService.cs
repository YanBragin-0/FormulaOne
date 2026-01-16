using Elastic.Clients.Elasticsearch;
using FormulaOne.Application.Dto.ElasticDto;
using FormulaOne.Application.Dto.Response;
using FormulaOne.Infrastructure.Storage;
using Microsoft.EntityFrameworkCore;

namespace FormulaOne.Application.Services.ElasticSearch
{
    public class AdminSearchService(ApplicationDbContext context, ElasticsearchClient client)
    {
        private readonly ApplicationDbContext _context = context;
        private readonly ElasticsearchClient _elastic = client;

        public async Task ElasticReindex()
        {
            await this.IndexConstrCShTable();
            await this.IndexDriverCShTable();
            await this.IndexCars();
            await this.IndexDrivers();
            await this.IndexRaces();
            await this.IndexCircuit();
            await this.IndexSeason();
            await this.IndexTeams();
        }
        private async Task IndexConstrCShTable()
        {
            var teamsTable = await _context.ConstructorsChampionship
                            .AsNoTracking()
                            .Include(t => t.Season)
                            .OrderByDescending(t => t.Points).ToListAsync();


            var docs = teamsTable.Select(t => new SearchDocument
            {
                Id = t.Id.ToString(),
                DocType = $"constructorsChampionship {t.Season!.Year}",
                Title = $"Кубок конструкторов {t.Season!.Year} года",
                Description = $"Итоговая таблица,(класификация) команд {t.Season.Year} года",
                SearchText = string.Join(" ", t.Season.Year.ToString(), "кубок конструкторов",
                                                            string.Join(" ", t.TeamName,t.Points,t.Season.Year))

            }).ToList();

            await _elastic.BulkAsync(bulk => bulk.Index("global").IndexMany(docs));
        }
        private async Task IndexCars()
        {
            var cars = await _context.Cars
                            .AsNoTracking()
                            .Include(c => c.Team)
                            .ToListAsync();

            var docs = cars.Select(c => new SearchDocument
            { 
                Id = c.Id.ToString(),
                DocType = "car",
                Title = c.Title,
                Description = $"{c.Team!.TeamName ?? string.Empty},{c.Description}",
                SearchText = string.Join(" ",c.Title,c.Team.TeamName,c.Description)
            }).ToList();

            await _elastic.BulkAsync(bulk => bulk.Index("global").IndexMany(docs));

        }
        private async Task IndexDriverCShTable()
        {
            var driversTable = await _context.DriverChampionship
                            .AsNoTracking()
                            .Include(d => d.Season)
                            .OrderByDescending(d => d.Points).ToListAsync();
            var docs = driversTable.Select(d => new SearchDocument
            {
                Id = d.Id.ToString(),
                DocType = $"driversChampionship {d.Season!.Year}",
                Title = $"Кубок пилотов {d.Season!.Year} года",
                Description = $"Итоговая таблица(классификация) пилотов {d.Season!.Year} года",
                SearchText = string.Join(" ", d.Season!.Year, "кубок пилотов",
                                                            string.Join(" ", d.Points, d.DriverName, d.TeamName, d.Season.Year))
            }).ToList();

            await _elastic.BulkAsync(bulk => bulk.Index("global").IndexMany(docs));
        }

        private async Task IndexDrivers()
        {
            var drivers = await _context.Drivers.AsNoTracking().ToListAsync();
            var teams = await _context.Teams.AsNoTracking().ToListAsync();
            var forDocs = (from d in drivers
                          join t in teams
                            on d.TeamId equals t.Id
                          select new
                          {
                              d.Id,
                              d.Name,
                              d.Age,
                              d.Country,
                              t.TeamName,
                              d.Biography
                          }).ToList();
            var docs = forDocs.Select(d => new SearchDocument
            {
                Id = d.Id.ToString(),
                DocType = "driver",
                Title = $"{d.Name},{d.Country}",
                Description = $"Пилот {d.Name} команды {d.TeamName}",
                SearchText = string.Join(" ", "пилот",
                                string.Join(" ",d.Name, d.Age, d.TeamName, d.Country, (d.Biography ?? string.Empty)))
            }).ToList();

            await _elastic.BulkAsync(b => b.Index("global").IndexMany(docs));
        }
        private async Task IndexRaces()
        {
            var races = await _context.Races
                            .AsNoTracking()
                            .Include(r => r.RaceCircuit)
                            .ToListAsync();
            var docs = races.Select(r => new SearchDocument 
            { 
                Id = r.Id.ToString(),
                DocType = "race",
                Title = r.Title,
                Description = $"{r.Title},{r.RaceCircuit!.CountryLocation}",
                SearchText = string.Join(" ",r.Title,r.RaceCircuit.Title,
                                    string.Join(" ",r.RaceCircuit.Length,r.RaceCircuit.CountryLocation))
            }).ToList();

            await _elastic.BulkAsync(b => b.Index("global").IndexMany(docs));
        }
        private async Task IndexCircuit()
        {
            var circuits = await _context.RaceCircuits
                                .AsNoTracking()
                                .Include(c => c.Race)
                                .ToListAsync();
            var docs = circuits.Select(c => new SearchDocument 
            { 
                Id = c.Id.ToString(),
                DocType = "circuit",
                Title = c.Title,
                Description = $"{c.Title},{c.CountryLocation}",
                SearchText = string.Join(" ", c.Title,c.CountryLocation,
                                    string.Join(" ",c.Length,c.Race!.Title,c.Race.DateTime))
            }).ToList();

            await _elastic.BulkAsync(b => b.Index("global").IndexMany(docs));
        }
        private async Task IndexSeason()
        {
            var seasons = await _context.Seasons.AsNoTracking().ToListAsync();
            var teamsCSh = await (from t in _context.ConstructorsChampionship
                                 select new
                                  {
                                      t.SeasonId,
                                      t.TeamName,
                                      t.Points
                                  })
                                 .ToListAsync();

            var driversCSh = await (from d in _context.DriverChampionship
                                    select new
                                    {
                                        d.SeasonId,
                                        d.DriverName,
                                        d.TeamName,
                                        d.Points
                                    })
                                    .ToListAsync();
            var includeCars = await _context.Cars.AsNoTracking().Include(c => c.Team).ToListAsync();
            var cars = (from c in includeCars
                        join s in seasons
                              on c.SeasonId equals s.Id
                        select new
                        {
                            c.SeasonId,
                            c.Title,
                            c.Team!.TeamName,
                            s.Year

                        }).ToList();
                              
            var docs = seasons.Select(s => new SearchDocument 
            { 
                Id = s.Id.ToString(),
                DocType = "season",
                Title = $"сезон {s.Year}",
                Description = $"командный зачёт, личный зачёт сезона {s.Year}",
                SearchText = string.Join(" ",s.Year.ToString(),
                                   string.Join(" ",teamsCSh.Where(t => t.SeasonId == s.Id).Select(t => $"{t.TeamName},{t.Points}")),
                                   string.Join(" ", driversCSh.Where(d => d.SeasonId == s.Id).Select(d => $"{d.DriverName},{d.TeamName},{d.Points}")),
                                   string.Join(" ", cars.Where(c => c.SeasonId == s.Id).Select(c => $"{c.Title},{c.TeamName},{c.Year}"))
                            )
            }).ToList();

            await _elastic.BulkAsync(b => b.Index("global").IndexMany(docs));
        }
        private async Task IndexTeams()
        {
            var teams = await _context.Teams
                               .AsNoTracking()
                               .Include(t => t.Car)
                               .ToListAsync();
            var docs = teams.Select(t => new SearchDocument
            {
                Id = t.Id.ToString(),
                DocType = "team",
                Title = $"{t.TeamName}",
                Description = $"команда {t.TeamName}",
                SearchText = string.Join(" ", "команда формулы 1", string.Join(" ", t.TeamName, t.Car!.Title, t.Biography))
            }).ToList();

            await _elastic.BulkAsync(b => b.Index("global").IndexMany(docs));
        }
    }
}
