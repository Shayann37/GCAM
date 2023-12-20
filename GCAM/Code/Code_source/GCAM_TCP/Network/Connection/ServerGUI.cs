using System.Net.Sockets;
using System.Net;

namespace GCAM_TCP
{
    internal class ServerGUI : IDisposable
    {
        internal int Port { get; set; }
        internal bool StopServer { get; set; }
        internal Socket Socket { get; set; }

        internal ServerGUI(int port) 
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
            ClientGUI clientGUI = await AcceptSocketAsync();
            if (!this.StopServer)
            {
                await AcceptClient();
            }
            else 
            {
                return;
            }
        }

        private async Task<ClientGUI> AcceptSocketAsync() 
        {
            if (!this.StopServer)
            {
                return new ClientGUI(await this.Socket.AcceptAsync());
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
