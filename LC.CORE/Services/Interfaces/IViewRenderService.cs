using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LC.CORE.Services.Interfaces
{
    public interface IViewRenderService
    {
        Task<string> RenderToStringAsync(string viewName, object model);
    }
}
