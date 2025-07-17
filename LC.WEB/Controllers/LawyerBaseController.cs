using LC.WEB.Filters.Lawyer;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LC.WEB.Controllers
{
    [LawyerValidatedFilterAttribute]
    public class LawyerBaseController : Controller
    {
    }
}
