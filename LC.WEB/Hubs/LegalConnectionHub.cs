using LC.CORE.Helpers;
using LC.ENTITIES.Models;
using LC.SERVICE.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LC.WEB.Hubs
{
    public class LegalConnectionHub : Hub
    {
        private readonly INotificationService _notificationService;

        public LegalConnectionHub(
            INotificationService notificationService
            )
        {
            _notificationService = notificationService;
        }
        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception ex)
        {
            await base.OnDisconnectedAsync(ex);
        }
       
    }
}
