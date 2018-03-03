namespace BGTBackend.Models
{
    public class Project
    {
        public string Id { get; set; }
        
        public string Status { get; set; }
        
        public string Description { get; set; }
        
        public string Category { get; set; }
        
        public int DaysPassed { get; set; }
        
        public int Surface { get; set; }
        
        public int Points { get; set; }
    }
}