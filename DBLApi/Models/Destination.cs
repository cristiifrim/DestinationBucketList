namespace DBLApi.Models
{
    public class Destination
    {
        protected int Id { get; set; }
        protected string Geolocation { get; set; } = default!;
        protected string Title { get; set; } = default!;
        protected string Image { get; set; } = default!;
        protected string Description { get; set; } = default!;
        protected DateOnly StartDate { get; set; }
        protected DateOnly EndDate { get; set; } 
    }
}