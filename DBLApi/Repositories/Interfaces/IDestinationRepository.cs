using DBLApi.Models;

namespace DBLApi.Repositories.Interfaces
{
    public interface IDestinationRepository : IGenericRepository<Destination>
    {
        public Task<bool> DestinationTitleExists(string title);
        public Task<Destination?> GetDestinationByTitle(string title);
        public Task<bool> DestinationExists(string geolocation);
        public Task<bool> AddUserDestination(int userId, int destinationId, DateTime startDate, DateTime endDate);

        public Task<ICollection<Destination>> GetPublicDestinations(int page = 1, int pageSize = 10);
        public Task<ICollection<Destination>> GetUserDestinations(int userId, int page = 1, int pageSize = 10);
    }
}