// See https://aka.ms/new-console-template for more information
using System;

namespace ConsoleToWebApi
{
    class Program
    {
        static void Main(string[] args)
        {
                CreateHostBuilder(args).Build().Run();
            Console.WriteLine("Hello World");
        }
        public static IHostBuilder CreateHostBuilder(string[] args) => 
        Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(webHost => {
                webHost.UseStartup<Startup>();
            });
    }
}