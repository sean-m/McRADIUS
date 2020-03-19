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
    }
}
