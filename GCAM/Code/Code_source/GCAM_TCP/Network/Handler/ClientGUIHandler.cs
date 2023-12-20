using GCAM_Common;
using System.Text;

namespace GCAM_TCP
{
    internal static class ClientGUIHandler
    {
        internal static async Task HandlePacket(ClientGUI clientGUI, byte[] packetReceived)
        {
            PacketType packetType = (PacketType)packetReceived[0];
            switch (packetType)
            {
                case PacketType.LOGIN_REQUEST:
                    LoginRequestPacket loginRequest = (LoginRequestPacket)Assembler.PacketDisassembler(packetReceived);

                    if (loginRequest.Password == "Administrator")
                    {
                        clientGUI.Id = Helper.BytesToHex(Encryption.SHA512SignatureWithSalt(Encoding.Unicode.GetBytes(clientGUI.IP), Encoding.Unicode.GetBytes(loginRequest.Password)));
                        Program.Client.Add(clientGUI.Id, clientGUI);
                        LoginResponsePacket loginResponsePacket = new LoginResponsePacket(PacketType.LOGIN_RESPONSE);
                        loginResponsePacket.Connected = true;
                        loginResponsePacket.Id = clientGUI.Id;
                        loginResponsePacket.NumberOfCameras = Program.Camera.Count;
                        loginResponsePacket.CameraId = new int[Program.Camera.Count];
                        int i = 0;
                        foreach(KeyValuePair<int, ClientESP> camera in Program.Camera)
                        {
                            loginResponsePacket.CameraId[i] = camera.Value.LocalId;
                            i++;
                        }
                        await clientGUI.SendPacketAsync(loginResponsePacket.Serialize());

                        /*DataRawPacket dataRawPacket = new DataRawPacket(PacketType.DATA_RAW);
                        DataPictureFromCamera dataPictureFromCamera = new DataPictureFromCamera(DataType.VIDEO_FLUX);
                        dataPictureFromCamera.CameraID = 12;

                        List<string> strings = new List<string>();
                        strings.Add("123.png");
                        strings.Add("456.jpg");
                        strings.Add("789.jpg");

                        while (true)
                        {

                            dataPictureFromCamera.Image = File.ReadAllBytes(strings[new Random().Next(0, strings.Count)]);
                            dataRawPacket.Data = dataPictureFromCamera.Serialize();
                            byte[] dataSerialized = dataRawPacket.Serialize();
                            await clientGUI.SendPacketAsync(dataSerialized);
                            Console.WriteLine("sent");
                            Thread.Sleep(1000);
                        }*/
                    }
                    break;

                case PacketType.ADD_CLIENT:
                    AddClientPacket addClientPacket = (AddClientPacket)Assembler.PacketDisassembler(packetReceived);
                    Program.Client[addClientPacket.ClientId].CameraClient.Add(addClientPacket.CameraId,clientGUI);
                    Console.WriteLine($"ClientId : {addClientPacket.ClientId} CameraId : {addClientPacket.CameraId}");
                    break;
            }
        }
    }
}
