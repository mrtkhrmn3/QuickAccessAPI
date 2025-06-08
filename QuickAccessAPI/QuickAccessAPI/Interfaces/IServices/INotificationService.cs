using QuickAccessAPI.Dto;

namespace QuickAccessAPI.Interfaces.IServices
{
    public interface INotificationService
    {
        Task SendNotificationAsync(CreateNotificationDTO dto, Guid userId);
        Task<IEnumerable<Notification>> GetNotificationsForSecurityAsync(string siteName);
        Task<bool> UpdateNotificationAsync(NotificationUpdateDTO dto);
        Task<bool> CompleteNotificationAsync(NotificationCompleteDTO dto);
        Task<bool> DeleteNotificationAsync(Guid id);
        Task<IEnumerable<Notification>> GetActiveNotificationsForSecurityAsync(Guid userId);
        Task<IEnumerable<Notification>> GetActiveNotificationsForResidentAsync(Guid userId);
        Task<Notification> GetNotificationById(Guid id);

    }
}
