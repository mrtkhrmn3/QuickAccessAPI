namespace QuickAccessAPI.Interfaces.IRepositories
{
    public interface ISecurityRepository : IGenericRepository<Security>
    {
        Task<IEnumerable<Security>> GetBySiteNameAsync(string siteName);
    }
}
