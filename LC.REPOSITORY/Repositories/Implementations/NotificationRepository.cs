using LC.CORE.Extensions;
using LC.DATABASE.Data;
using LC.ENTITIES.Custom;
using LC.ENTITIES.Models;
using LC.REPOSITORY.Base;
using LC.REPOSITORY.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.REPOSITORY.Repositories.Implementations
{
    public class NotificationRepository : Repository<Notification>, INotificationRepository
    {
        public NotificationRepository(LegalConnectionContext context) : base(context) { }

        public async Task<NotificationCustomModel> GetNotificationsByUser(string userId)
        {
            var details = await _context.UserNotifications.Where(x => x.UserId == userId)
                .OrderByDescending(x=>x.Notification.CreatedAt)
                .Select(x => new NotificationDetailCustomModel
                {
                    Id = x.Id,
                    CreatedAt = x.Notification.CreatedAt.ElapsedTime(),
                    IsRead = x.IsRead,
                    Text = x.Notification.Text,
                    Url = x.Notification.Url
                })
                .ToListAsync();

            var model = new NotificationCustomModel
            {
                Total = details.Count(),
                Pending = details.Where(x=>!x.IsRead).Count(),
                Details = details
            };

            return model;
        }

        public async Task ReadUserNotification(Guid id)
        {
            var userNotification = await _context.UserNotifications.Where(x => x.Id == id).FirstOrDefaultAsync();
            userNotification.IsRead = true;
            userNotification.ReadDate = DateTime.UtcNow;

            await _context.SaveChangesAsync();
        }
    }
}
