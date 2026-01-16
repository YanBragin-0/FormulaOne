namespace FormulaOne.Entities
{
    public class Race
    {
        public Guid Id { get; set; }
        public Guid CircuitId { get; set; }
        public DateTime? DateTime { get; set; }
        public string Title { get; set; } = null!;
        public RaceCircuit? RaceCircuit { get; set; }
        private Race()
        {
            
        }
        public Race(Guid circuitId,string Title,DateTime? dateTime)
        {
            Id = Guid.NewGuid();
            this.Title = Title;
            CircuitId = circuitId;
            DateTime = dateTime;
        }

    }
}
