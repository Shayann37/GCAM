using System.Net.Sockets;

namespace GCAM_TCP
{
    internal class ClientGUI : IDisposable
    {
        internal Socket Socket { get; set; }
        internal bool IsConnected { get; set; }
        internal Dictionary<int, ClientGUI> CameraClient {  get; set; }

        internal string IP
        {
            get
            {
                return this.Socket.RemoteEndPoint.ToString();
            }
        }

        internal string Id { get; set; }

        internal ClientGUI(Socket socket)
        {
            this.Socket = socket;
            this.IsConnected = true;
            this.CameraClient = new Dictionary<int, ClientGUI>();
            Task.Run(ReceivePacketAsync);
        }

        private async Task ReceivePacketAsync()
        {

            byte[] packet = await StartReceivePacket();
            if (packet != null)
            {
                Console.WriteLine($"Packet : {packet.Length}");
                //Log.LogInfo("Packet received !", false);
                //await ()ConfigureAwaitFalse();to threat data;
                await ClientGUIHandler.HandlePacket(this, packet).ConfigureAwait(false);
            }

            if (IsConnected)
            {
                await ReceivePacketAsync();
            }
            else
            {
                this.Dispose();
                return;
            }
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

                    int size = BitConverter.ToInt32(header, 0);

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
                catch (Exception ex)
                {
                    if (ex is SocketException)
                    {
                        this.IsConnected = false;
                        this.Dispose();
                    }
                    return null;
                }
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
                        if (ex is SocketException)
                        {
                            this.IsConnected = false;
                            this.Dispose();
                        }
                    }
                }
                return false;
            });
            return sent;
        }


        public void Dispose()
        {
            if (Socket != null)
            {
                Socket?.Close();
                Socket?.Dispose();
                Socket = null;
            }
            GC.SuppressFinalize(this);
        }
    }
}
