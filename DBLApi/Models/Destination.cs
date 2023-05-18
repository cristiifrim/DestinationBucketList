namespace DBLApi.Models
{
    public class Destination
    {
        protected int Id { get; set; }
        protected string? Geolocation { get; set; }
        protected string? Title { get; set; }
        protected string? Image { get; set; }
        protected string? Description { get; set; }
        protected DateTime? StartDate { get; set; }
        protected DateTime? EndDate { get; set; }
    }
}