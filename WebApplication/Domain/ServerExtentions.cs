namespace WebApplication.Domain
{
    using System.Net;
    using System.Net.Sockets;
    using System.Text;

    public static class ServerExtentions
    {
        public static object GetServerData(this Server server)
        {
            try
            {
                byte[] bytes = new byte[256];

                Socket s = new Socket(AddressFamily.InterNetwork,
                    SocketType.Stream, ProtocolType.Tcp);

                s.Connect(IPAddress.Parse(server.IpAddress), server.Port);

                s.Send(Encoding.UTF8.GetBytes("Test"), 2, SocketFlags.None);

                var retVal = s.Receive(bytes);

                var data = Encoding.UTF8.GetString(bytes);

                return new { };
            }
            catch (System.Exception)
            {

                return new { };
            }
        }
    }
}