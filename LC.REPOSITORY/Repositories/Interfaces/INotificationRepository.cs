using LC.ENTITIES.Custom;
using LC.ENTITIES.Models;
using LC.REPOSITORY.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LC.REPOSITORY.Repositories.Interfaces
{
    public interface INotificationRepository : IRepository<Notification>
    {
        Task<NotificationCustomModel> GetNotificationsByUser(string userId);
        Task ReadUserNotification(Guid id);
    }
}
