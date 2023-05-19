using DBLApi.Models;

namespace DBLApi.DTOs
{
    public class DestinationDTO
    {
        public string Geolocation { get; set; } = default!;
        public string Title { get; set; } = default!;
        public string Image { get; set; } = default!;
        public string Description { get; set; } = default!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; } 

        public DestinationDTO DestinationToDTO(Destination destination)
        {
            return new DestinationDTO
            {
                Geolocation = destination.Geolocation,
                Title = destination.Title,
                Image = destination.Image,
                Description = destination.Description,
                StartDate = destination.StartDate,
                EndDate = destination.EndDate
            };
        }
    }
}