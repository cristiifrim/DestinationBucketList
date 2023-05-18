using DBLApi.Models;

namespace DBLApi.Repositories.Interfaces
{
    public interface IDestinationRepository : IGenericRepository<Destination>
    {
        public Task<bool> DestinationExists(string title);
    }
}