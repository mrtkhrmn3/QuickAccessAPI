namespace QuickAccessAPI.Interfaces.IRepositories
{
    public interface INotificationRepository : IGenericRepository<Notification>
    {
        Task<IEnumerable<Notification>> GetBySiteNameAsync(string siteName);

        Task<IEnumerable<Notification>> GetActiveNotificationsBySiteAsync(string siteName);

    }
}
