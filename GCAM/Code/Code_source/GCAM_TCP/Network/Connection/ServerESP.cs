using System.Net.Sockets;
using System.Net;

namespace GCAM_TCP
{
    internal class ServerESP : IDisposable
    {
        internal int Port { get; set; }
        internal bool StopServer { get; set; }
        internal Socket Socket { get; set; }

        internal ServerESP(int port)
        {
            this.Port = port;
            this.StopServer = false;
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, port);
            this.Socket = new Socket(endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                this.Socket.Bind(endPoint);
                this.Socket.Listen(int.MaxValue);
                Task.Run(AcceptClient);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private async Task AcceptClient()
        {
            ClientESP clientESP = await AcceptSocketAsync();
            if (!this.StopServer)
            {
                await AcceptClient();
            }
            else
            {
                return;
            }
        }

        private async Task<ClientESP> AcceptSocketAsync()
        {
            if (!this.StopServer)
            {
                return new ClientESP(await this.Socket.AcceptAsync());
            }
            else
                return null;
        }

        public void Dispose()
        {
            this.Socket.Close();
            this.Socket.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
