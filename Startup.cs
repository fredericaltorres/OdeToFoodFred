using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace OdeToFood
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IGreeter, Greeter>(); // once instance for the all app
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

            app.UseMvcWithDefaultRoute();
                
            // For every request
            app.Run(async (context) => {

                // priority form low to high: appSettings.json, Env Var, command line parameter
                // dotnet.exe run Greeting="Hello from command line"
                var greeting = configuration["Greeting"];
                greeting = greeter.GetMessageOfTheDay();
                await context.Response.WriteAsync($"Message:{greeting}  EnvironmentName:{env.EnvironmentName}");
            });
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
