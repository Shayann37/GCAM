namespace GCAM_TCP
{
    internal class Program
    {
        internal static Dictionary<int, ClientESP> Camera { get; set; }
        internal static Dictionary<string, ClientGUI> Client { get; set; }

        static Program() 
        {
            Program.Camera = new Dictionary<int, ClientESP>();
            Program.Client = new Dictionary<string, ClientGUI>();
        }

        static void Main(string[] args)
        {
            new Thread(() => new ServerGUI(5959)).Start();
            new Thread(() => new ServerESP(5960)).Start();
            //new Thread(() => {  }).Start();
            while (true)
            {
                Console.ReadLine();
            }
        }   
    }
}