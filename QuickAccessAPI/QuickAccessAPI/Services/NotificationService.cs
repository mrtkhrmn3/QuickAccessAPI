using QuickAccessAPI.Dto;
using QuickAccessAPI.Interfaces.IRepositories;
using QuickAccessAPI.Interfaces.IServices;

namespace QuickAccessAPI.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;

        public NotificationService(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public async Task SendNotificationAsync(CreateNotificationDTO dto, Guid userId)
        {
            var notification = new Notification
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Block = dto.Block,
                AptNo = dto.AptNo,
                Type = dto.Type,
                Status = "Active",
                Description = dto.Description,
                CreatedAt = DateTime.Now,
                SiteName = dto.SiteName
            };

            await _notificationRepository.AddAsync(notification);
            await _notificationRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<Notification>> GetNotificationsForSecurityAsync(string siteName)
        {
            var all = await _notificationRepository.GetAllAsync();
            return all.Where(n => n.SiteName == siteName);
        }

        public async Task<bool> UpdateNotificationAsync(NotificationUpdateDTO dto)
        {
            var notification = await _notificationRepository.GetByIdAsync(dto.Id);
            if (notification == null)
                return false;

            notification.Status = dto.Status;
            if (!string.IsNullOrEmpty(dto.Description))
                notification.Description = dto.Description;

            _notificationRepository.Update(notification);
            return await _notificationRepository.SaveChangesAsync();
        }

        public async Task<bool> DeleteNotificationAsync(Guid id)
        {
            var notification = await _notificationRepository.GetByIdAsync(id);
            if (notification == null)
                return false;

            _notificationRepository.Delete(notification);
            return await _notificationRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<Notification>> GetActiveNotificationsForSecurityAsync(string siteName)
        {
            return await _notificationRepository.GetActiveNotificationsBySiteAsync(siteName);
        }
    }
}
