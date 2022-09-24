using CookEat.Api.Configuration;

namespace CookEat.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplication
                .CreateBuilder(args)
                .ConfigureServices()
                .Build()
                .ConfigureMiddlewares()
                .Run();
        }
    }
}