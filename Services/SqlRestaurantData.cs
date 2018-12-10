using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OdeToFood.Data;
using OdeToFood.Models;
using Microsoft.EntityFrameworkCore;

namespace OdeToFood.Services
{
    public class SqlRestaurantData : IRestaurantData
    {
        private OdeToFoodDbContext _context;

        public SqlRestaurantData(OdeToFoodDbContext context)
        {
            this._context = context;
        }

        public Restaurant Add(Restaurant restaurant)
        {
            this._context.Restaurants.Add(restaurant);
            this._context.SaveChanges(); // Populate the field restaurant.Id
            return restaurant;
        }

        public Restaurant Get(int id)
        {
            return this._context.Restaurants.FirstOrDefault( r => r.Id ==id);
        }

        public IEnumerable<Restaurant> GetAll()
        {
            // Only work for small records
            return this._context.Restaurants.OrderBy(r => r.Name);
        }

        public Restaurant Update(Restaurant restaurant)
        {
            this._context.Attach(restaurant).State = EntityState.Modified;
            this._context.SaveChanges();
            return restaurant;
        }
    }
}
