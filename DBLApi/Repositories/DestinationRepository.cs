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
    }
}