using Flexinets.Net;
using Flexinets.Radius;
using Flexinets.Radius.Core;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace McRADIUS
{
    public class Server
    {
        private ServerConfiguration config;
        private bool running = false;
        private string[] args;
        private Task aspNetServer;
        private CancellationTokenSource cancelSource = new CancellationTokenSource();
        private IPacketHandler handler;
        private RadiusServer authenticationServer;
        private RadiusServer accountingServer;

        private ILogger serverLogger;

        public Server()
        {

        }

        public void Init(ServerConfiguration config, string[] args)
        {
            Console.WriteLine("Initializing...");

            if (running) throw new Exception("Server already running! Cannot initialize server.");

            this.args = args;
            this.config = config;

            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddFilter("Microsoft", LogLevel.Warning)
                    .AddFilter("System", LogLevel.Warning)
                    .AddFilter("LoggingConsoleApp.Program", LogLevel.Debug)
                    .AddConsole();
#if WINDOWS
                builder.AddEventLog();
#endif
            });

            serverLogger = loggerFactory.CreateLogger<Server>();
        }

        public void Run()
        {
            Console.WriteLine("Hello world!");

            try
            {
                // Startup Radius server
                // TODO config for port, secret and the like
                var path = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory) + "/Content/radius.dictionary";
                var dictionary = new RadiusDictionary(path, NullLogger<RadiusDictionary>.Instance);
                var radiusPacketParser = new RadiusPacketParser(NullLogger<RadiusPacketParser>.Instance, dictionary);
                var packetHandler = config.PacketHandler;
                var repository = new PacketHandlerRepository();
                var udpClientFactory = new UdpClientFactory();
                repository.AddPacketHandler(IPAddress.Any, packetHandler, config.SharedSecret);

                
                authenticationServer = new RadiusServer(
                    udpClientFactory,
                    new IPEndPoint(IPAddress.Any, 1812),
                    radiusPacketParser,
                    RadiusServerType.Authentication,
                    repository,
                    NullLogger<RadiusServer>.Instance);

                authenticationServer.Start();

                if (config.UseAccountingServer)
                {
                    accountingServer = new RadiusServer(
                       udpClientFactory,
                       new IPEndPoint(IPAddress.Any, 1813),
                       radiusPacketParser,
                       RadiusServerType.Accounting,
                       repository,
                       NullLogger<RadiusServer>.Instance);
                    accountingServer.Start();
                }


                // Startup ASP.NET server
                aspNetServer = CreateHostBuilder(args).Build().RunAsync(cancelSource.Token);
            }
            catch (Exception ex)
            {
                serverLogger.LogCritical($"Exception through while starting services:\n{ex.Message}");
            }

        }

        public void Stop()
        {
            // Signal stop ASP.NET service
            if (!cancelSource.IsCancellationRequested) cancelSource.Cancel();

            // Stop Radius
            if (authenticationServer != null) {
                authenticationServer.Stop();
                authenticationServer.Dispose();
            }
            if (accountingServer != null)
            {
                accountingServer.Stop();
                accountingServer.Dispose();
            }
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
