using QuickAccessAPI.Interfaces.IRepositories;

namespace QuickAccessAPI.Repositories
{
    public class ResidentRepository : GenericRepository<Resident>, IResidentRepository
    {
        public ResidentRepository(QuickAccessDbContext context) : base(context) { }
    }
}
