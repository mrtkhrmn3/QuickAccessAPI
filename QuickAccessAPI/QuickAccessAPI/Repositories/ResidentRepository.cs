using Microsoft.EntityFrameworkCore;
using QuickAccessAPI.Interfaces.IRepositories;

namespace QuickAccessAPI.Repositories
{
    public class ResidentRepository : GenericRepository<Resident>, IResidentRepository
    {
        public ResidentRepository(QuickAccessDbContext context) : base(context) { }
        public async Task<IEnumerable<Resident>> GetBySiteNameAsync(string siteName)
        {
            return await _dbSet
                .AsNoTracking()
                .Where(r => r.SiteName == siteName)
                .ToListAsync();
        }
    }
}
