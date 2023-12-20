
namespace GCAM_Common
{
    internal class DataRawPacket : BasePacket
    {
        internal byte[] Data { get; set; }

        internal DataRawPacket(PacketType packetType) : base(packetType) { }

        internal override byte[] Serialize()
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(ms))
                {
                    writer.Write((byte)this.PacketType);
                    writer.Write(this.Data.Length);
                    writer.Write(this.Data);
                    return ms.ToArray();
                }
            }
        }

        internal override DataRawPacket Deserialize(byte[] dataRaw)
        {
            using (MemoryStream ms = new MemoryStream(dataRaw))
            {
                using (BinaryReader reader = new BinaryReader(ms))
                {
                    DataRawPacket packet = new DataRawPacket((PacketType)dataRaw[0]);
                    packet.PacketType = (PacketType)reader.ReadByte();
                    int sizeOfData = reader.ReadInt32();
                    packet.Data = reader.ReadBytes(sizeOfData);
                    return packet;
                }
            }
        }
    }
}
