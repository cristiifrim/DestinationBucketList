namespace DBLApi.Models
{
    public class StayDates
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int UserId { get; set; }
        public User User { get; set; } = default!;

        public int DestinationId { get; set; }
        public Destination Destination { get; set; } = default!;
    }
}