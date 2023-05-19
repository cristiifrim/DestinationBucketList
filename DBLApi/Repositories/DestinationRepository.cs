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

        public async Task<bool> AddUserDestination(int userId, int destinationId, DateTime startDate, DateTime endDate)
        {
            _context.StayDates.Add(new StayDates
            {
                UserId = userId,
                DestinationId = destinationId,
                StartDate = startDate,
                EndDate = endDate
            });
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DestinationExists(string title)
        {
            return await _context.Destinations.AnyAsync(d => d.Title == title && d.IsPublic);
        }

        public Task<bool> DestinationTitleExists(string title)
        {
            return _context.Destinations.AnyAsync(d => d.Title == title && d.IsPublic);
        }

        public Task<Destination?> GetDestinationByTitle(string title)
        {
            return _context.Destinations.FirstOrDefaultAsync(d => d.Title == title);
        }

        public async Task<ICollection<Destination>> GetPublicDestinations(int page = 1, int pageSize = 10)
        {
            return await _context.Destinations
                .Where(d => d.IsPublic)
                .OrderBy(d => d.Title)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<ICollection<Destination>> GetUserDestinations(int userId, int page = 1, int pageSize = 10)
        {
            return await _context.Destinations
                .Include(d => d.StayDates)
                .Where(d => d.StayDates.Any(sd => sd.UserId == userId))
                .OrderBy(d => d.Title)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
    }
}