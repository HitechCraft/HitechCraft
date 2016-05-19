namespace WebApplication.Core
{
    using System;
    using System.IO;
    using System.Net.Sockets;
    using System.Text;
    public class MinecraftServerStatusManager
    {
        //TODO переписать это убожество....

        const ushort dataSize = 512;
        const ushort numFields = 6; 
        private string address;
        private ushort port;
        private bool serverUp;
        private string motd;
        private string version;
        private string currentPlayers;
        private string maximumPlayers;

        public MinecraftServerStatusManager(string address, ushort port)
        {
            byte[] rawServerData = new byte[dataSize];
            string[] serverData;

            SetAddress(address);
            SetPort(port);

            try
            {
                // ToDo: Add timeout
                TcpClient tcpclient = new TcpClient();
                tcpclient.Connect(address, port);
                Stream stream = tcpclient.GetStream();
                byte[] payload = { 0xFE, 0x01 };
                stream.Write(payload, 0, payload.Length);
                stream.Read(rawServerData, 0, dataSize);
                tcpclient.Close();
            }
            catch (Exception)
            {
                serverUp = false;
                return;
            }

            if (rawServerData == null || rawServerData.Length == 0)
                serverUp = false;
            else
            {
                serverData = Encoding.Unicode.GetString(rawServerData).Split("\u0000\u0000\u0000".ToCharArray());
                if (serverData != null && serverData.Length >= numFields)
                {
                    serverUp = true;
                    SetVersion(serverData[2]);
                    SetMotd(serverData[3]);
                    SetCurrentPlayers(serverData[4]);
                    SetMaximumPlayers(serverData[5]);
                }
                else
                    serverUp = false;
            }
        }

        public string GetAddress()
        {
            return address;
        }

        public void SetAddress(string address)
        {
            this.address = address;
        }

        public ushort GetPort()
        {
            return port;
        }

        public void SetPort(ushort port)
        {
            this.port = port;
        }

        public string GetMotd()
        {
            return motd;
        }

        public void SetMotd(string motd)
        {
            this.motd = motd;
        }

        public string GetVersion()
        {
            return version;
        }

        public void SetVersion(string version)
        {
            this.version = version;
        }

        public string GetCurrentPlayers()
        {
            return currentPlayers;
        }

        public void SetCurrentPlayers(string currentPlayers)
        {
            this.currentPlayers = currentPlayers;
        }

        public string GetMaximumPlayers()
        {
            return maximumPlayers;
        }

        public void SetMaximumPlayers(string maximumPlayers)
        {
            this.maximumPlayers = maximumPlayers;
        }

        public bool IsServerUp()
        {
            return serverUp;
        }
    }
}