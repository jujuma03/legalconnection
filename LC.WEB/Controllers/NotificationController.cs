using LC.SERVICE.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LC.WEB.Controllers
{
    [Authorize]
    [Route("notificaciones")]
    public class NotificationController : Controller
    {
        private readonly INotificationService _notificationService;
        private readonly IUserService _userService;

        public NotificationController(
            INotificationService notificationService,
            IUserService userService
            )
        {
            _notificationService = notificationService;
            _userService = userService;
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetNotifications()
        {
            var user = await _userService.GetUserByClaim(User);
            var notifications = await _notificationService.GetNotificationsByUser(user.Id);
            return PartialView("Partials/NotificationPartialView",notifications);
        }

        [HttpGet("read-notification")]
        public async Task<IActionResult> ReadNotification(Guid id)
        {
            await _notificationService.ReadUserNotification(id);
            return Ok();
        }
    }
}
