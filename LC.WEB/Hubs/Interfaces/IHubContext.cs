using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LC.WEB.Hubs.Interfaces
{
    public interface IHubContext
    {
        Task SendNotification(string message, string link, params string[] userIds);
        Task CloseSessionToLawyer(params string[] userIds);
    }
}
