using LocalHiringPlatform.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalHiringPlatform.Domain.Interfaces
{
    public interface INotificationService
    {
        Task CreateAsync(Guid userId, string title, string message);
        Task<List<NotificationModel>> GetMyNotificationsAsync(Guid userId);
    }
}
