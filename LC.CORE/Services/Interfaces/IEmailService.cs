using LC.CORE.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LC.CORE.Services.Interfaces
{
    public interface IEmailService
    {
        Task SendEmail(string subject, string message, params string[] emails);
    }
}
