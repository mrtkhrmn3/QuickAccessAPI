using Microsoft.EntityFrameworkCore;
using QuickAccessAPI.Interfaces.IRepositories;

namespace QuickAccessAPI.Repositories
{
    public class SecurityRepository : GenericRepository<Security>, ISecurityRepository
    {
        public SecurityRepository(QuickAccessDbContext context) : base(context) { }

        public async Task<IEnumerable<Security>> GetBySiteNameAsync(string siteName)
        {
            return await _dbSet
                .AsNoTracking()
                .Where(s => s.SiteName == siteName)
                .ToListAsync();
        }
    }
}
