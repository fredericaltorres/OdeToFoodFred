using Microsoft.AspNetCore.Mvc;
using OdeToFood.Models;
using OdeToFood.Services;
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
        private IRestaurantData _restaurantData;

        public HomeController(IRestaurantData restaurantData)
        {
            this._restaurantData = restaurantData;
        }

        public IActionResult index()
        {
            // return Content("This is the home page ~ Hello from the home controller");
            var model = this._restaurantData.GetAll();

            // The return type of this object is defined in the pipeline, the default is json
            // Search for: Content Negotiation
            //return new ObjectResult(model);
            return View(model);
        }
    }
}
