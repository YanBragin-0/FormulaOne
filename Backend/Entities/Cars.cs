namespace FormulaOne.Entities
{
    public class Cars
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = string.Empty;
        public Guid SeasonId { get; set; }
        public Guid TeamId { get; set; }
        public Teams? Team { get; set; } // navigation for 1 : 1
        private Cars()
        {
            
        }
        public Cars(Guid TeamId,Guid SeasonId,string Title,string Description = "")
        {
            Id = Guid.NewGuid();
            this.SeasonId = SeasonId;
            this.TeamId = TeamId;
            this.Title = Title;
            this.Description = Description;
        }

    }
}
