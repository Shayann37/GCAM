
namespace GCAM_Common
{
    internal enum DataType : byte 
    {
        NUMBER_OF_CAMERA = 0x0,
        VIDEO_FLUX = 0x1,

        UNDEFINED = 0xFF
    }

    internal abstract class BaseData
    {
        protected internal DataType DataType { get; set; } 

        protected BaseData(DataType dataType) 
        {
            this.DataType = dataType;
        }

        internal abstract byte[] Serialize();
        internal abstract BaseData Deserialize(byte[] data);
    }
}
