namespace GCAM_Common
{
    internal class DataPictureFromCamera : BaseData
    {
        internal byte[] Image { get; set; }
        internal int CameraID { get; set; }

        internal DataPictureFromCamera(DataType dataType) : base(dataType)
        {}

        internal override byte[] Serialize()
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(ms))
                {
                    writer.Write((byte)this.DataType);
                    writer.Write(this.CameraID);
                    writer.Write(this.Image.Length);
                    writer.Write(this.Image);
                    return ms.ToArray();
                }
            }
        }

        internal override DataPictureFromCamera Deserialize(byte[] dataPicture)
        {
            using (MemoryStream ms = new MemoryStream(dataPicture))
            {
                using (BinaryReader reader = new BinaryReader(ms))
                {
                    DataPictureFromCamera dataPictureFromCamera = new DataPictureFromCamera((DataType)dataPicture[0]);
                    dataPictureFromCamera.DataType = (DataType)reader.ReadByte();
                    dataPictureFromCamera.CameraID = reader.ReadInt32();
                    int sizeOfPicture = reader.ReadInt32();
                    dataPictureFromCamera.Image = reader.ReadBytes(sizeOfPicture);
                    return dataPictureFromCamera;
                }
            }
        }
    }
}
