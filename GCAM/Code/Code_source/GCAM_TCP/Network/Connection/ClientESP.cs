using GCAM_Common;
using System.Net.Sockets;

namespace GCAM_TCP
{
    internal class ClientESP : IDisposable
    {
        private static int Id { get; set; }

        static ClientESP() 
        {
            ClientESP.Id = 0;
        }

        internal Socket Socket { get; set; }
        internal bool IsConnected { get; set; }
        internal int LocalId { get; set; }

        internal ClientESP(Socket socket)
        {
            this.Socket = socket;
            this.IsConnected = true;
            this.LocalId = ClientESP.Id;
            ClientESP.Id++;
            Program.Camera.Add(this.LocalId, this);
            Task.Run(ReceivePacketAsync);
        }

        private async Task ReceivePacketAsync()
        {

            byte[] packet = await StartReceivePacket();

            await Task.Run(async () => {
                if (packet != null)
                {
                    if (packet.Length > 0)
                    {
                        await Task.Run(async () => 
                        {
                            DataRawPacket dataRawPacket = new DataRawPacket(PacketType.DATA_RAW);
                            DataPictureFromCamera dataPictureFromCamera = new DataPictureFromCamera(DataType.VIDEO_FLUX);
                            dataPictureFromCamera.Image = packet;
                            dataPictureFromCamera.CameraID = this.LocalId;
                            dataRawPacket.Data = Assembler.DataAssembler(dataPictureFromCamera);

                            foreach (KeyValuePair<string, ClientGUI> clientGUI in Program.Client) 
                            {
                                foreach (KeyValuePair<int, ClientGUI> camera in clientGUI.Value.CameraClient) 
                                {
                                    await camera.Value.SendPacketAsync(Assembler.PacketAssembler(dataRawPacket));
                                }
                                //await clientGUI.Value.SendPacketAsync(packet);
                            }
                        });
                        //Console.WriteLine($"Packet : {packet.Length}");

                       // File.WriteAllBytes("test.jpg", packet);
                        //await StartSendPacket(new byte[] { 0x0 }).ConfigureAwait(false);
                    }
                }
            }).ConfigureAwait(false);

       
            if (!IsConnected)
            {
                this.Dispose();
                return;
            }

            await ReceivePacketAsync();
        }

        public async Task<byte[]> StartReceivePacket()
        {
            byte[] packet = await Task.Run(() =>
            {
                try
                {
                    int total = 0;
                    int recv;
                    byte[] header = new byte[4];
                    this.Socket.Poll(-1, SelectMode.SelectRead);
                    recv = this.Socket.Receive(header, 0, 4, 0);
                    if (recv > 0)
                    {
                        int size = BitConverter.ToInt32(header, 0);
                        Console.WriteLine($"Size : {size}");
                        int dataleft = size;
                        byte[] data = new byte[size];
                        while (total < size)
                        {
                            recv = this.Socket.Receive(data, total, dataleft, 0);
                            total += recv;
                            dataleft -= recv;
                        }
                        return data;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("LOL");
                    this.IsConnected = false;
                    this.Dispose();
                }
                return null;
            });
            return packet;
        }

        internal async Task<bool> SendPacketAsync(byte[] packet)
        {
            bool sent = await StartSendPacket(packet);
            if (sent)
            {

                //await ()ConfigureAwaitFalse();to threat data;
                //await MasterDataHandler.Reader(packet).ConfigureAwait(false);
                //await ReceivePacketAsync();
            }
            else
            {
                this.Dispose();
            }
            return sent;
        }

        public async Task<bool> StartSendPacket(byte[] packet)
        {
            bool sent = await Task.Run(() =>
            {
                int total = 0;
                int size = packet.Length;
                int datalft = size;
                byte[] header = BitConverter.GetBytes(size);

                lock (this.Socket)
                {
                    try
                    {
                        this.Socket.Poll(-1, SelectMode.SelectWrite);
                        int sent = this.Socket.Send(header);

                        if (size > 1000000)
                        {
                            using (MemoryStream memoryStream = new MemoryStream(packet))
                            {
                                int read = 0;
                                memoryStream.Position = 0;
                                byte[] chunk = new byte[50 * 1000];
                                while ((read = memoryStream.Read(chunk, 0, chunk.Length)) > 0)
                                {
                                    this.Socket.Send(chunk, 0, read, SocketFlags.None);
                                }
                            }
                        }
                        else
                        {
                            while (total < size)
                            {
                                sent = this.Socket.Send(packet, total, size, SocketFlags.None);
                                total += sent;
                                datalft -= sent;
                            }
                        }
                        return true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("LOL");
                        this.IsConnected = false;
                        this.Dispose();
                    }
                }
                return false;
            });
            return sent;
        }


        public void Dispose()
        {
            //Socket?.Shutdown(SocketShutdown.Both);
            Socket.Close();
            Socket.Dispose();
            Socket = null;
            GC.SuppressFinalize(this);
        }
    }
}
