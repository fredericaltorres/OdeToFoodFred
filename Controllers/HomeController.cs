using Microsoft.AspNetCore.Mvc;
using OdeToFood.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OdeToFood.Controllers
{
    /// <summary>
    /// Automatically mapped to the root of the web site by MVC
    /// </summary>
    public class HomeController : Controller
    {
        public IActionResult index()
        {
            // return Content("This is the home page ~ Hello from the home controller");
            var model = new Restaurant { Id = 1, Name = "Scott's pizza place" };

            // The return type of this object is defined in the pipeline, the default is json
            // Search for: Content Negotiation
            //return new ObjectResult(model);

            return View(model);
        }
    }
}
