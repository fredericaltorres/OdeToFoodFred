﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using OdeToFood.Data;
using OdeToFood.Services;


namespace OdeToFood
{
    public class Startup
    {
        IConfiguration _configuration;

        public Startup(IConfiguration configuration)   // << Dependency injection)
        {
            this._configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IGreeter, Greeter>(); // Once instance for the all app

            // services.AddScoped<IRestaurantData, InMemoryRestaurantData>(); // Once instance for the all app
            // services.AddSingleton<IRestaurantData, InMemoryRestaurantData>(); // Only work because we have an in memory database

            // Configure Entity Framework
            // DbContext is not thread safe so create an instance for each http call
            services.AddDbContext<OdeToFoodDbContext>( options =>
                options.UseSqlServer(_configuration.GetConnectionString("OdeToFoodConnectionString"))
            );
            services.AddScoped<IRestaurantData, SqlRestaurantData>(); // Only work because we have an in memory database

            //services.AddTransient // allocated each time requested
            //services.AddScoped // For every http request
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app,        // << Dependency injection
            IHostingEnvironment env,        // << Dependency injection
            IConfiguration configuration,   // << Dependency injection
            IGreeter greeter,               // << Dependency injection
            ILogger<Startup> logger
            )
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage(); // Must be registered first in the pipeline
            }
            else
            {
                // app.UseExceptionHandler();  
            }

            // Use static file located in folde wwww and set up default page
            //app.UseDefaultFiles(); // Already to index.html, must be set before UseStaticFiles
            app.UseStaticFiles();
            // Same as above
            //app.UseFileServer();

            // app.UseMvcWithDefaultRoute(); << default route configured
            app.UseMvc(ConfigureRoutes);
                
            // For every request
            app.Run(async (context) => {

                // priority form low to high: appSettings.json, Env Var, command line parameter
                // dotnet.exe run Greeting="Hello from command line"
                var greeting = configuration["Greeting"];
                greeting = greeter.GetMessageOfTheDay();
                context.Response.ContentType = "text/plain";
                await context.Response.WriteAsync($"Not Found - Message:{greeting} EnvironmentName:{env.EnvironmentName}");
            });
        }
        private void ConfigureRoutes(IRouteBuilder routeBuilder)
        {
            // All this url go to the home control index method
            // https://localhost:44328/home/index/1
            // https://localhost:44328/home/index
            // https://localhost:44328/home
            // https://localhost:44328/
            routeBuilder.MapRoute("Default", "{controller=Home}/{action=index}/{id?}"); // id is optional
        }
    }
}

/*
    // Custom in place middleware
    app.Use(next =>  {

        return async context => {

            logger.LogInformation("Request icoming");
            if(context.Request.Path.StartsWithSegments("/mym"))
            {
                await context.Response.WriteAsync("Hit!!");
                logger.LogInformation("Request handled");
                // Do not call the next middleware
            }
            else
            {
                await next(context); // Call the next middleware
                logger.LogInformation("Response outgoing");
            }
        };
    });
    app.UseWelcomePage(new WelcomePageOptions { Path = "/wp" });     
*/

    