using Domain;
using Presentation.Services.Interfaces;

namespace Presentation.Services
{
    public class DbService : IDbService
    {
        private readonly ApplicationContext _context;
        public DbService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task Clear()
        {
            await _context.DisposeAsync();
            await _context.SaveChangesAsync();
        }
    }
}
