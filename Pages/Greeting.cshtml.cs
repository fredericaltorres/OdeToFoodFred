using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OdeToFood.Services;

namespace OdeToFood.Pages
{
    public class GreetingModel : PageModel
    {
        private IGreeter _greeter;
        public string CurrentGreeting { get;set;}

        public GreetingModel(IGreeter greeter)
        {
            _greeter = greeter;
        }
        /// <summary>
        ///  On Http get
        /// </summary>
        public void OnGet(string name)
        {
            this.CurrentGreeting = $"{name}:{_greeter.GetMessageOfTheDay()}";
        }
    }
}