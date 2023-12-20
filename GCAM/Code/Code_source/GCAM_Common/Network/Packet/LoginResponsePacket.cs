
namespace GCAM_Common
{
    internal class LoginResponsePacket : BasePacket
    {
        internal bool Connected { get; set; }
        internal LoginResponsePacket(PacketType packetType) : base(packetType) { }
        internal string Id { get; set; }
        internal int NumberOfCameras { get; set; }
        internal int[] CameraId { get; set; }

        internal override byte[] Serialize()
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(ms))
                {
                    writer.Write((byte)this.PacketType);
                    writer.Write(this.Connected);
                    writer.Write(this.Id);
                    writer.Write(this.NumberOfCameras);
                    foreach(int id in CameraId)
                        writer.Write(id);
                    return ms.ToArray();
                }
            }
        }

        internal override LoginResponsePacket Deserialize(byte[] loginRequest)
        {
            using (MemoryStream ms = new MemoryStream(loginRequest))
            {
                using (BinaryReader reader = new BinaryReader(ms))
                {
                    LoginResponsePacket packet = new LoginResponsePacket((PacketType)loginRequest[0]);
                    packet.PacketType = (PacketType)reader.ReadByte();
                    packet.Connected = reader.ReadBoolean();
                    packet.Id = reader.ReadString();
                    packet.NumberOfCameras = reader.ReadInt32();
                    packet.CameraId = new int[packet.NumberOfCameras];
                    for (int i = 0; i < packet.NumberOfCameras; i++)
                        packet.CameraId[i] = reader.ReadInt32();
                    return packet;
                }
            }
        }
    }
}
