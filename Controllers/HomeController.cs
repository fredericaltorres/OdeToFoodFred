using Microsoft.AspNetCore.Mvc;
using OdeToFood.Models;
using OdeToFood.Services;
using OdeToFood.ViewModels;
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
        private readonly IGreeter _greeter;

        public HomeController(IRestaurantData restaurantData, IGreeter greeter)
        {
            this._restaurantData = restaurantData;
            this._greeter = greeter;
        }

        public IActionResult index()
        {
            // return Content("This is the home page ~ Hello from the home controller");
            var model = new HomeIndexViewModel()
            {
                Restaurants = this._restaurantData.GetAll(),
                CurrentMessage = _greeter.GetMessageOfTheDay()
            };

            // The return type of this object is defined in the pipeline, the default is json
            // Search for: Content Negotiation
            //return new ObjectResult(model);
            return View(model);
        }

        public IActionResult Details(int id)
        {
            var model = this._restaurantData.Get(id);
            return View(model);
        }
    }
}
