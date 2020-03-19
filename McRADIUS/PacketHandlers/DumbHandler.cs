using System;
using Flexinets.Radius.Core;

namespace McRADIUS.PacketHandlers
{
    public class DumbHandler : IPacketHandler, IDisposable
    {
        public DumbHandler()
        {
        }

        void IDisposable.Dispose()
        {
            throw new NotImplementedException();
        }

        IRadiusPacket IPacketHandler.HandlePacket(IRadiusPacket packet)
        {
            switch (packet.Code)
            {
                case PacketCode.AccessRequest:
                    // TODO request and respond with Access-Accept
                    var username = packet.Attributes["User-Name"].ToString() ?? "NO NAME";
                    Console.WriteLine($"{DateTime.Now.ToString("o")} : Access-Request for {username}");
                    var response = packet.CreateResponsePacket(PacketCode.AccessAccept);
                    return response;
                default:
                    throw new NotImplementedException($"Nothing implemented for code type: {packet.Code}");
            }
        }
    }
}
