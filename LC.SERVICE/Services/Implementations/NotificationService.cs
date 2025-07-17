using LC.ENTITIES.Custom;
using LC.ENTITIES.Models;
using LC.REPOSITORY.Repositories.Interfaces;
using LC.SERVICE.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LC.SERVICE.Services.Implementations
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;

        public NotificationService(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public async Task<NotificationCustomModel> GetNotificationsByUser(string userId)
            => await _notificationRepository.GetNotificationsByUser(userId);

        public async Task Insert(Notification entity)
            => await _notificationRepository.Insert(entity);

        public async Task ReadUserNotification(Guid id)
            => await _notificationRepository.ReadUserNotification(id);
    }
}
