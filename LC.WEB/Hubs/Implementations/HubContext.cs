using LC.ENTITIES.Models;
using LC.SERVICE.Services.Interfaces;
using LC.WEB.Hubs.Interfaces;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LC.WEB.Hubs.Implementations
{
    public class HubContext : IHubContext
    {
        private readonly IHubContext<LegalConnectionHub> _hubContext;
        private readonly INotificationService _notificationService;

        public HubContext(
            IHubContext<LegalConnectionHub> hubContext,
            INotificationService notificationService
            )
        {
            _hubContext = hubContext;
            _notificationService = notificationService;
        }
        public async Task SendNotification(string message, string link, params string[] userIds)
        {
            try
            {
                var notification = new Notification
                {
                    CreatedAt = DateTime.UtcNow,
                    Text = message,
                    Url = link,
                    UserNotifications = userIds.Select(x => new UserNotification
                    {
                        UserId = x
                    }).ToList()
                };

                await _notificationService.Insert(notification);

                foreach (var userId in userIds)
                {
                    try
                    {
                        await _hubContext.Clients.User(userId).SendAsync("receivemessage",message);
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        public async Task CloseSessionToLawyer(params string[] userIds)
        {
            foreach (var userId in userIds)
            {
                try
                {
                    await _hubContext.Clients.User(userId).SendAsync("closesession");
                }
                catch (Exception)
                {
                    continue;
                }
            }
        }
    }
}
