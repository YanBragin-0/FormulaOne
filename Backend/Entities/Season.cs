namespace FormulaOne.Entities
{
    public class Season
    {
        public Guid Id { get; set; }
        public short Year { get; set; }
        public ConstructorsChampionship? ConstructorsChampionship { get; set; }
        public DriverChampionship? DriverChampionship {  set; get; } 
        private Season()
        {
            
        }
        public Season(short Year)
        {
            Id = Guid.NewGuid();
            this.Year = Year;
        }
    }
}
