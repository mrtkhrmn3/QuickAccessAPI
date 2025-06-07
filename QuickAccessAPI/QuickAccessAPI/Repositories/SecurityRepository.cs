using QuickAccessAPI.Interfaces.IRepositories;

namespace QuickAccessAPI.Repositories
{
    public class SecurityRepository : GenericRepository<Security>, ISecurityRepository
    {
        public SecurityRepository(QuickAccessDbContext context) : base(context) { }
    }
}
