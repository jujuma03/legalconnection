using LC.CORE.Services.Models;
using LC.WEB.Services.Google.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LC.WEB.Services.Google.Interfaces
{
    public interface IGoogleService
    {
        Task<GoogleUser> GetUserByGoogleTokenId(string googleTokenId);
    }
}
