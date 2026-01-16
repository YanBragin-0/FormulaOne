namespace FormulaOne.Entities
{
    public class Teams
    {
        public Guid Id { get; set; }
        public string TeamName { get; set; } = null!;
        public string? Biography { get; set; }
        public Cars? Car {  get; set; } // nav for 1 : 1
        private Teams()
        {
            
        }
        public Teams(string TeamName,string? Buigraphy)
        {
            Id = Guid.NewGuid();
            this.TeamName = TeamName;
            this.Biography = Buigraphy;
        }
        public void AddBiography(string Biography) => this.Biography = Biography;

    }
}
