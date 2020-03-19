using System;

namespace McRADIUS_Server
{
    class Program
    {
        static void Main(string[] args)
        {
            // Parse args and setup config
            var config = new McRADIUS.ServerConfiguration();

            var server = new McRADIUS.Server();
            server.Init(config, args);
            server.Run();

            Console.WriteLine("Press enter to stop service.");

            Console.ReadLine();

            server.Stop();
        }
    }
}
