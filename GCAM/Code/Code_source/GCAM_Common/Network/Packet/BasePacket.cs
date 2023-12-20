
namespace GCAM_Common
{
    internal enum PacketType : byte
    {
        LOGIN_REQUEST = 0x0,
        LOGIN_RESPONSE = 0x1,
        OPEN_CONNECTION = 0x3,
        DATA_RAW = 0x4,
        ADD_CLIENT = 0x5,
    }

    internal abstract class BasePacket
    {
        protected PacketType PacketType { get; set; }

        protected BasePacket(PacketType packetType) 
        {
            this.PacketType = packetType;
        }

        internal abstract byte[] Serialize();
        internal abstract BasePacket Deserialize(byte[] packet);
    }
}
