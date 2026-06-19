using LocalHiringPlatform.Api.Extensions;
using LocalHiringPlatform.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LocalHiringPlatform.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationController
        : ControllerBase
    {
        private readonly
            INotificationService
            _notificationService;

        public NotificationController(
            INotificationService
                notificationService)
        {
            _notificationService =
                notificationService;
        }

        [HttpGet("my")]
        public async Task<IActionResult> GetMyNotifications()
        {
            Guid userId = User.GetUserId();

            var notifications =
                await _notificationService
                    .GetMyNotificationsAsync(
                        userId);

            return Ok(
                notifications);
        }
    }
}
