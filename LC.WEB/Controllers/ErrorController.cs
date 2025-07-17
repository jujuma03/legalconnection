using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LC.WEB.Controllers
{
    [AllowAnonymous]
    [Route("[controller]")]

    public class ErrorController : Controller
    {
        public ErrorController()
        {

        }

        [HttpGet("{statusCode}")]
        public IActionResult Index(int statusCode)
        {
            this.Response.StatusCode = statusCode;
            return this.View(statusCode);
        }
    }
}
