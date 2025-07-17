using System;
using System.Collections.Generic;
using System.Text;

namespace LC.ENTITIES.Custom
{
    public class NotificationCustomModel
    {
        public int Total { get; set; }
        public int Pending { get; set; }
        public List<NotificationDetailCustomModel> Details { get; set; }
    }
}
