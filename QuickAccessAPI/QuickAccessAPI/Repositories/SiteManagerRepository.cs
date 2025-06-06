using QuickAccessAPI.Interfaces.IRepositories;

namespace QuickAccessAPI.Repositories
{
    public class SiteManagerRepository : GenericRepository<SiteManager>, ISiteManagerRepository
    {
        public SiteManagerRepository(QuickAccessDbContext context) : base(context) { }
    }
}
