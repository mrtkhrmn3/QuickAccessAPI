using Microsoft.EntityFrameworkCore;
using QuickAccessAPI.Interfaces.IRepositories;

namespace QuickAccessAPI.Repositories
{
    public class NotificationRepository : GenericRepository<Notification>,INotificationRepository
    {
        public NotificationRepository(QuickAccessDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Notification>> GetBySiteNameAsync(string siteName)
        {
            return await _dbSet
                .AsNoTracking()
                .Where(n => n.SiteName == siteName)
                .OrderByDescending(n => n.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Notification>> GetActiveNotificationsBySiteAsync(string siteName)
        {
            var lowerSiteName = siteName.Trim().ToLower();

            return await _dbSet
                .AsNoTracking()
                .Where(n => n.Status == "Active" &&
                            !string.IsNullOrEmpty(n.SiteName) &&
                            n.SiteName.Trim().ToLower() == lowerSiteName)
                .OrderByDescending(n => n.CreatedAt)
                .ToListAsync();
        }

    }
}
