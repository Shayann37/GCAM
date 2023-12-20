namespace GCAM_Common
{
    internal static class Assembler
    {
        internal static byte[] PacketAssembler(BasePacket basePacket)
        {
            return basePacket.Serialize();
        }

        internal static BasePacket PacketDisassembler(byte[] basePacket)
        {
            PacketType packetType = (PacketType)basePacket[0];
            switch (packetType) 
            {
                case PacketType.LOGIN_REQUEST:
                    return new LoginRequestPacket(packetType).Deserialize(basePacket);

                case PacketType.LOGIN_RESPONSE:
                    return new LoginResponsePacket(packetType).Deserialize(basePacket);

                case PacketType.DATA_RAW:
                    return new DataRawPacket(packetType).Deserialize(basePacket);

                case PacketType.ADD_CLIENT:
                    return new AddClientPacket(packetType).Deserialize(basePacket);

                default:
                    return null;
            }
        }

        internal static byte[] DataAssembler(BaseData baseData)
        {
            return baseData.Serialize();
        }

        internal static BaseData DataDisassembler(byte[] baseData)
        {
            DataType dataType = (DataType)baseData[0];

            switch (dataType)
            {
                case DataType.NUMBER_OF_CAMERA:
                    return new DataNumberOfCamera(dataType).Deserialize(baseData);

                case DataType.VIDEO_FLUX:
                    return new DataPictureFromCamera(dataType).Deserialize(baseData);
                default:
                    return null;
            }
        }
    }
}
