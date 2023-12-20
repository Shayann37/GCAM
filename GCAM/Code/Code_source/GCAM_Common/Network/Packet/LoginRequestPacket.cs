
namespace GCAM_Common
{
    internal class LoginRequestPacket : BasePacket
    {
        internal string Password { get; set; }
        internal LoginRequestPacket(PacketType packetType) : base(packetType) { }

        internal override byte[] Serialize()
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(ms))
                {
                    writer.Write((byte)this.PacketType);
                    writer.Write(this.Password);
                    return ms.ToArray();
                }
            }
        }

        internal override LoginRequestPacket Deserialize(byte[] loginRequest) 
        {
            using (MemoryStream ms = new MemoryStream(loginRequest))
            {
                using (BinaryReader reader = new BinaryReader(ms))
                {
                    LoginRequestPacket packet = new LoginRequestPacket((PacketType)loginRequest[0]);
                    packet.PacketType = (PacketType)reader.ReadByte();
                    packet.Password = reader.ReadString();
                    return packet;
                }
            }
        }
    }
}