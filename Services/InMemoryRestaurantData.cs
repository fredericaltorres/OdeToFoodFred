using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OdeToFood.Models;

namespace OdeToFood.Services
{
    public class InMemoryRestaurantData : IRestaurantData
    {
        /// <summary>
        /// FYI: List are not thread safe
        /// </summary>
        private List<Restaurant> _restaurant;

        public InMemoryRestaurantData()
        {
            _restaurant = new List<Restaurant>() {
                new Restaurant { Id = 1, Name = "Scott's pizza place" },
                new Restaurant { Id = 2, Name = "Tersiguels" },
                new Restaurant { Id = 3, Name = "King's Contrivance" },
            };
        }

        public Restaurant Add(Restaurant newRestaurant)
        {
            newRestaurant.Id = this._restaurant.Max(r => r.Id) + 1;
            _restaurant.Add(newRestaurant);
            return newRestaurant;
        }

        public Restaurant Get(int id)
        {
            return this._restaurant.FirstOrDefault(r => r.Id == id);
        }

        public IEnumerable<Restaurant> GetAll()
        {
            return this._restaurant.OrderBy(r => r.Name);
        }
    }
}
