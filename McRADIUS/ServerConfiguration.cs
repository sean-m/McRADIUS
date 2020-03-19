using System;
using Flexinets.Radius.Core;

namespace McRADIUS
{
    public class ServerConfiguration
    {
        public ServerConfiguration() { }

        public IPacketHandler PacketHandler { get; set; }
        public string SharedSecret { get; set; }
        public bool UseAccountingServer { get; set; } = false;
        public string IPAddress { get; set; } = "0.0.0.0";
    }
}
