using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LC.WEB.Models.Hubs
{
    public class NotificationHubModel
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public string UserId { get; set; }
        public string Link { get; set; }
    }
}
