using QuickAccessAPI.Dto;
using QuickAccessAPI.Interfaces.IRepositories;
using QuickAccessAPI.Interfaces.IServices;

namespace QuickAccessAPI.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IGenericRepository<Notification> _notificationRepository;

        public NotificationService(IGenericRepository<Notification> notificationRepository)
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
                Status = "Pending",
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
    }
}
