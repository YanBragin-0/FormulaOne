

namespace FormulaOne.Entities
{
    public class DriverChampionship
    {
        public Guid Id { get; set; }
        public Guid SeasonId { get; set; }
        public Guid DriverId { get; set; }
        public string DriverName { get; set; } = null!;
        public string TeamName { get; set; } = null!;
        public int Points { get; set; }
        public Season? Season { get; set; }
        private DriverChampionship()
        {
            
        }
        public DriverChampionship(Guid seasonId,Guid driverId,string NameDriver,string teamName,int points)
        {
            this.SeasonId = seasonId;
            this.DriverId = driverId;
            DriverName = NameDriver;
            TeamName = teamName;
            Points = points;
        }
    }
}
