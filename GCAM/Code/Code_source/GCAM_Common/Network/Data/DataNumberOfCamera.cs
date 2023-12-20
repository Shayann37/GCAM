

namespace GCAM_Common
{
    internal class DataNumberOfCamera : BaseData
    {
        internal int NumberOfCamera { get; set; }

        internal DataNumberOfCamera(DataType dataType) : base(dataType) { }

        internal override byte[] Serialize()
        {
            return null;
        }

        internal override BaseData Deserialize(byte[] data)
        {
            return null;
        }
    }
}
