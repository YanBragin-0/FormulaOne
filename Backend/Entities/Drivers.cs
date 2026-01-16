namespace FormulaOne.Entities
{
    public class Drivers
    {
        public Guid Id { get; set; }
        public Guid? TeamId { get; set; }
        public string Name { get; set; } = null!;
        public short Age { get; set; }
        public string Country { get; set; } = null!;
        public string? Biography { get; set; }
        private Drivers()
        {
            
        }
        public Drivers(Guid TeamId,string Name,short Age, string Country,string? Biography)
        {
            Id = Guid.NewGuid();
            this.TeamId = TeamId;
            this.Name = Name;
            this.Age = Age;
            this.Country = Country;
            this.Biography = Biography;
        }

    }
  
}
