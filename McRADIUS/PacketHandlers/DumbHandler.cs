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
            throw new NotImplementedException();
        }
    }
}
