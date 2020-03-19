using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;

namespace McRADIUS
{
    public class Server
    {
        private ServerConfiguration _config;
        private bool _running = false;


        public Server()
        {

        }

        public void Init(ServerConfiguration config)
        {
            Console.WriteLine("Initializing...");

            if (_running) throw new Exception("Server already running! Cannot initialize server.");
            _config = config;
        }

        public void Run()
        {
            Console.WriteLine("Hello world!");
            //CreateHostBuilder(args).Build().Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
