using System;
using System.Collections.Generic;
using System.Text;

namespace LC.ENTITIES.Models
{
    public class UserNotification
    {
        public Guid Id { get; set; }
        public Guid NotificationId { get; set; }
        public string UserId { get; set; }
        public bool IsRead { get; set; }
        public DateTime? ReadDate { get; set; }
        public ApplicationUser User { get; set; }
        public Notification Notification { get; set; }
    }
}
