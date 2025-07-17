using LC.ENTITIES.Custom;
using LC.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LC.SERVICE.Services.Interfaces
{
    public interface INotificationService
    {
        Task Insert(Notification entity);
        Task<NotificationCustomModel> GetNotificationsByUser(string userId);
        Task ReadUserNotification(Guid id);
    }
}
