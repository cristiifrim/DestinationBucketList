using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using DBLApi.Models;

namespace DBLApi.DTOs
{
    public class DestinationDto
    {
        [Required]
        public string Geolocation { get; set; } = default!;
        [Required]
        public string Title { get; set; } = default!;
        [Required]
        public string Image { get; set; } = default!;
        [Required]
        public string Description { get; set; } = default!; 

        public DateTime StartDate { get; set; } = DateTime.MinValue;
        public DateTime EndDate { get; set; } = DateTime.MinValue;

        [JsonConstructor]
        public DestinationDto(){}

        public DestinationDto(Destination destination)
        {
            Geolocation = destination.Geolocation;
            Title = destination.Title;
            Image = destination.Image;
            Description = destination.Description;
        }

        public static Destination ToDestination(DestinationDto destinationDTO)
        {
            return new Destination
            {
                Geolocation = destinationDTO.Geolocation,
                Title = destinationDTO.Title,
                Image = destinationDTO.Image,
                Description = destinationDTO.Description,
            };
        }

        public static DestinationDto ToDestinationDto(Destination destination)
        {
            return new DestinationDto
            {
                Geolocation = destination.Geolocation,
                Title = destination.Title,
                Image = destination.Image,
                Description = destination.Description,
            };
        }

        public static StayDates ToStayDates(DestinationDto destinationDTO)
        {
            return new StayDates
            {
                StartDate = destinationDTO.StartDate,
                EndDate = destinationDTO.EndDate
            };
        }
    }
}