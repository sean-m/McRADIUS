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
        }
    }
}
