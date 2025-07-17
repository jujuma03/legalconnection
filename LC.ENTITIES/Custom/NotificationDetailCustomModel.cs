using System;
using System.Collections.Generic;
using System.Text;

namespace LC.ENTITIES.Custom
{
    public class NotificationDetailCustomModel
    {
        public Guid Id { get; set; }
        public bool IsRead { get; set; }
        public string Text { get; set; }
        public string CreatedAt { get; set; }
        public string Url { get; set; }
    }
}
