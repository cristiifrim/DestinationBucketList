using DBLApi.Data;
using DBLApi.Models;
using DBLApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DBLApi.Repositories
{
    public class DestinationRepository: GenericRepository<Destination>, IDestinationRepository
    {
        private readonly DataContext _context;
        public DestinationRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> DestinationExists(string title)
        {
            return await _context.FindAsync<Destination>(title) != null;
        }
    }
}