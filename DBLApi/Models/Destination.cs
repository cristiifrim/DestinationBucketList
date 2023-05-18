namespace DBLApi.Models
{
    public class Destination
    {
        public int Id { get; set; }
        public string Geolocation { get; set; } = default!;
        public string Title { get; set; } = default!;
        public string Image { get; set; } = default!;
        public string Description { get; set; } = default!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; } 
    }
}