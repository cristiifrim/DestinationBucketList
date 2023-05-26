namespace DBLApi.Models
{
    public class Destination
    {
        public int Id { get; set; }
        public string Geolocation { get; set; } = default!;
        public string Title { get; set; } = default!;
        public string Image { get; set; } = default!;
        public bool IsPublic { get; set; }
        public string Description { get; set; } = default!;

        public ICollection<StayDates> StayDates { get; set; } = default!;
    }
}