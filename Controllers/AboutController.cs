using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OdeToFood.Controllers
{
    /// <summary>
    /// Automatically mapped to the root of the web site by MVC
    /// </summary>
    [Route("[controller]")] // or use [Route("about")]
    public class AboutController : Controller
    {
        [Route("[action]")] // or [Route("phone")]
        public string Phone()
        {
            return "1+555+666+777";
        }
        [Route("[action]")] // or [Route("address")]
        public string Address()
        {
            return "USA";
        }
    }
}
