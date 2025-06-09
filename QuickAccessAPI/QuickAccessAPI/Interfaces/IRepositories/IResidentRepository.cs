namespace QuickAccessAPI.Interfaces.IRepositories
{
    public interface IResidentRepository : IGenericRepository<Resident>
    {
        Task<IEnumerable<Resident>> GetBySiteNameAsync(string siteName);
    }
}
