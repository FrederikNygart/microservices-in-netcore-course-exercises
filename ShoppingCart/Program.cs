using Microsoft.AspNetCore.Hosting;
using Serilog;

namespace ShoppingCart
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseSerilog()
                .UseKestrel()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
