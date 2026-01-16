namespace FormulaOne.Entities
{
    public class RaceCircuit
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string CountryLocation { get; set; } = string.Empty;
        public double Length { get; set; }
        public Race? Race { get; set; }
        private RaceCircuit()
        {
            
        }
        public RaceCircuit(string Title,string Country,double length)
        {
            Id = Guid.NewGuid();
            this.Title = Title;
            this.CountryLocation = Country;
            this.Length = length;
        }
    }
}
