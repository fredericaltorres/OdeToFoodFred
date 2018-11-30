using Microsoft.Extensions.Configuration;

namespace OdeToFood
{
    public interface IGreeter
    {
        string GetMessageOfTheDay();
    }

    public class Greeter : IGreeter
    {
        private IConfiguration _configuration;

        public Greeter(IConfiguration configuration) // << Dependency injection here too
        {
            this._configuration = configuration;
        }
        public string GetMessageOfTheDay()
        {
            return this._configuration["Greeting"];
        }
    }
}