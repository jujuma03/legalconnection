using System;
using System.Collections.Generic;
using System.Text;

namespace LC.ENTITIES.Models
{
    public class Notification
    {
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Guid Id { get; set; }
        public string Text { get; set; }
        public string Url { get; set; }
        public ICollection<UserNotification> UserNotifications  { get; set; }
    }
}
