using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuickAccessAPI.Dto;
using QuickAccessAPI.Interfaces.IServices;
using System.Security.Claims;

namespace QuickAccessAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpPost]
        [Authorize(Roles = "Resident")]
        public async Task<IActionResult> SendNotification([FromBody] CreateNotificationDTO dto)
        {
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!Guid.TryParse(userIdString, out var userId))
            {
                return Unauthorized("Invalid user ID in token.");
            }

            await _notificationService.SendNotificationAsync(dto, userId);
            return Ok("Notification sent successfully.");
        }

        [HttpGet("{siteName}")]
        public async Task<IActionResult> GetNotificationsForSecurity(string siteName)
        {
            var notifications = await _notificationService.GetNotificationsForSecurityAsync(siteName);
            return Ok(notifications);
        }

        [HttpGet("GetActiveNotifications/{siteName}")]
        [Authorize(Roles = "Security")]
        public async Task<IActionResult> GetActiveNotifications(string siteName)
        {
            var notifications = await _notificationService.GetActiveNotificationsForSecurityAsync(siteName);
            return Ok(notifications);
        }


        [HttpPut("UpdateNotification")]
        [Authorize(Roles = "Resident")]
        public async Task<IActionResult> UpdateNotification([FromBody] NotificationUpdateDTO dto)
        {
            var result = await _notificationService.UpdateNotificationAsync(dto);
            if (!result)
                return NotFound("Notification not found");

            return Ok("Notification updated successfully.");
        }

        [HttpDelete("DeleteNotification{id}")]
        [Authorize(Roles = "Resident")]
        public async Task<IActionResult> DeleteNotification(Guid id)
        {
            var success = await _notificationService.DeleteNotificationAsync(id);
            if (!success)
                return NotFound("Notification not found");

            return Ok("Notification deleted successfully.");
        }


    }
}
