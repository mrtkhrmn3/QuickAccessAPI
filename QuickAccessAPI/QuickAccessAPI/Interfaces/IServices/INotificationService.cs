using QuickAccessAPI.Dto;

namespace QuickAccessAPI.Interfaces.IServices
{
    public interface INotificationService
    {
        Task SendNotificationAsync(CreateNotificationDTO dto, Guid userId);
        Task<IEnumerable<Notification>> GetNotificationsForSecurityAsync(string siteName);
    }
}
