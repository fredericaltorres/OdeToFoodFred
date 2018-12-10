using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OdeToFood.Models;
using OdeToFood.Services;

namespace OdeToFood.Pages.Restaurants
{
    public class EditModel : PageModel
    {
        IRestaurantData _restaurantData;

        [BindProperty]
        public Restaurant Restaurant {get; set;}

        public EditModel(IRestaurantData restaurantData)
        {
            _restaurantData = restaurantData;
        }
        public IActionResult OnGet(int id)
        {
            this.Restaurant = _restaurantData.Get(id);
            if(this.Restaurant == null)
                return RedirectToAction("Index", "Home");  // Go back to home page because the restaurant id is invalid
            return Page(); // Same as View()
        }
        public IActionResult OnPost()
        {
            if(this.ModelState.IsValid)
            {
                this._restaurantData.Update(this.Restaurant);
                return RedirectToAction("Details", "Home", new { id = this.Restaurant.Id });
            }
            else
            {
                return Page();
            }
        }
    }
}