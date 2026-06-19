using LocalHiringPlatform.Domain.Entities;
using LocalHiringPlatform.Domain.Interfaces;
using LocalHiringPlatform.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalHiringPlatform.Infrastructure.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;

        private readonly IUnitOfWork _unitOfWork;

        public NotificationService(
            INotificationRepository
                notificationRepository,
            IUnitOfWork
                unitOfWork)
        {
            _notificationRepository =
                notificationRepository;

            _unitOfWork =
                unitOfWork;
        }

        public async Task CreateAsync(
            Guid userId,
            string title,
            string message)
        {
            var notification =
                new Notification
                {
                    UserId = userId,

                    Title = title,

                    Message = message,

                    IsRead = false,

                    CreatedOn =
                        DateTime.UtcNow
                };

            await _notificationRepository
                .AddAsync(notification);

            await _unitOfWork
                .SaveChangesAsync();
        }

    public async Task<List<NotificationModel>> GetMyNotificationsAsync(Guid userId)
        {
            var notifications =
                await _notificationRepository
                    .GetByUserIdAsync(userId);

            return notifications
                .Select(x =>
                    new NotificationModel
                    {
                        EntityId = x.EntityId,

                        Title = x.Title,

                        Message = x.Message,

                        IsRead = x.IsRead,

                        CreatedOn = x.CreatedOn
                    })
                .ToList();
        }
    }
}
