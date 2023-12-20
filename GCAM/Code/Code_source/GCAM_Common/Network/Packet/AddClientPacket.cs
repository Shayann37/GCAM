
namespace GCAM_Common
{
    internal class AddClientPacket : BasePacket
    {
        internal int CameraId { get; set; }
        internal string ClientId { get; set; }
        internal AddClientPacket(PacketType packetType) : base(packetType) { }

        internal override byte[] Serialize()
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(ms))
                {
                    writer.Write((byte)this.PacketType);
                    writer.Write(this.ClientId);
                    writer.Write(this.CameraId);
                    return ms.ToArray();
                }
            }
        }

        internal override AddClientPacket Deserialize(byte[] loginRequest)
        {
            using (MemoryStream ms = new MemoryStream(loginRequest))
            {
                using (BinaryReader reader = new BinaryReader(ms))
                {
                    AddClientPacket packet = new AddClientPacket((PacketType)loginRequest[0]);
                    packet.PacketType = (PacketType)reader.ReadByte();
                    packet.ClientId = reader.ReadString();
                    packet.CameraId = reader.ReadInt32();
                    return packet;
                }
            }
        }
    }
}
