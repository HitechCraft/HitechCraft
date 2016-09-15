namespace HitechCraft.Core.Helper
{
    #region Using Directives

    using System;
    using System.IO;
    using System.Net.Sockets;
    using System.Text;

    #endregion

    public class MinecraftServerHelper
    {
        //TODO переписать это убожество!!!!!

        const int dataSize = 512;
        const int numFields = 6; 
        private string address;
        private int port;
        private bool serverUp;
        private string motd;
        private string version;
        private int currentPlayers;
        private int maximumPlayers;

        public MinecraftServerHelper(string address, int port)
        {
            byte[] rawServerData = new byte[dataSize];
            string[] serverData;

            SetAddress(address);
            SetPort(port);

            try
            {
                // ToDo: Add timeout
                TcpClient tcpclient = new TcpClient();

                var result = tcpclient.BeginConnect(address, port, null, null);

                var success = result.AsyncWaitHandle.WaitOne(TimeSpan.FromSeconds(2));

                if (!success)
                {
                    throw new Exception("Failed to connect");
                }

                Stream stream = tcpclient.GetStream();

                byte[] payload = { 0xFE, 0x01 };
                stream.Write(payload, 0, payload.Length);
                stream.Read(rawServerData, 0, dataSize);
                tcpclient.Close();

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
                        SetCurrentPlayers(int.Parse(serverData[4]));
                        SetMaximumPlayers(int.Parse(serverData[5]));
                    }
                    else
                    {
                        serverUp = false;
                    }
                }
            }
            catch (Exception)
            {
                serverUp = false;
                return;
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

        public int GetPort()
        {
            return port;
        }

        public void SetPort(int port)
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

        public int GetCurrentPlayers()
        {
            return currentPlayers;
        }

        public void SetCurrentPlayers(int currentPlayers)
        {
            this.currentPlayers = currentPlayers;
        }

        public int GetMaximumPlayers()
        {
            return maximumPlayers;
        }

        public void SetMaximumPlayers(int maximumPlayers)
        {
            this.maximumPlayers = maximumPlayers;
        }

        public bool IsServerUp()
        {
            return serverUp;
        }
    }
}