using GCAM_Common;

namespace UnitTest
{
    [TestClass]
    public class LoginRequestPacketTest
    {
        [TestMethod]
        public void DataPictureFromCamera_Serialize()
        {
            DataPictureFromCamera dataPictureFromCamera = new DataPictureFromCamera(DataType.VIDEO_FLUX);
            dataPictureFromCamera.CameraID = 1;
            byte[] testRandomPicture = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13 };
            dataPictureFromCamera.Image = testRandomPicture;
            byte[] classSerialized = dataPictureFromCamera.Serialize();

            Assert.AreEqual<DataType>(DataType.VIDEO_FLUX, (DataType)classSerialized[0]);
            Assert.AreEqual<int>(1, BitConverter.ToInt32(new byte[] { classSerialized[1], classSerialized[2], classSerialized[3], classSerialized[4] }));
            Assert.AreEqual<int>(testRandomPicture.Length, BitConverter.ToInt32(new byte[] { classSerialized[5], classSerialized[6], classSerialized[7], classSerialized[8] }));
        }

        [TestMethod]
        public void DataPictureFromCameraTest_Deserialize()
        {
            DataPictureFromCamera dataPictureFromCamera = new DataPictureFromCamera(DataType.VIDEO_FLUX);
            dataPictureFromCamera.CameraID = 1;
            byte[] testRandomPicture = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13 };
            dataPictureFromCamera.Image = testRandomPicture;
            byte[] classSerialized = dataPictureFromCamera.Serialize();

            DataPictureFromCamera deserialized = new DataPictureFromCamera(DataType.UNDEFINED).Deserialize(classSerialized);

            Assert.AreEqual<DataType>(DataType.VIDEO_FLUX, deserialized.DataType);
            Assert.AreEqual<int>(1, deserialized.CameraID);
            Assert.AreEqual<int>(testRandomPicture.Length, deserialized.Image.Length);
        }
    }
}