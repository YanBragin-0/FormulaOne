namespace FormulaOne.Entities
{
    public class ConstructorsChampionship
    {
        public Guid Id { get; set; }
        public Guid SeasonId {  get; set; }
        public Guid TeamId { get; set; }
        public string TeamName { get; set; } = null!;
        public int Points { get; set; }
        public Season? Season { get; set; }
        private ConstructorsChampionship()
        {
            
        }
        public ConstructorsChampionship(Guid SeasonId, Guid TeamId,string TeamName, int points)
        {
            this.SeasonId = SeasonId;
            this.TeamId = TeamId;
            this.TeamName = TeamName;
            Points = points;
        }
        
    }
}
