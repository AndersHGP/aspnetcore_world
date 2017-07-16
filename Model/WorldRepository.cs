using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace WebApplication1.Model
{
    public class WorldRepository : IWorldRepository
    {
        private WorldContext _context;
        private ILogger<WorldRepository> _logger;

        public WorldRepository()
        {

        }

        public WorldRepository(WorldContext context, ILogger<WorldRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IEnumerable<T> GetAll<T>() where T : class
        {
            IQueryable<T> query = _context.Set<T>();

            _logger.LogInformation($"Getting all of type: {typeof(T)} from database.");
            
            return query.ToList();
        }
    }
}