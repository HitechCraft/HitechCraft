namespace DAL.Domain.Extentions
{
    using Json;

    //TODO: Убрать отсюда!!!
    public static class ServerExtentions
    {
        public static JsonMinecraftServerData GetServerData(this Server server)
        {
            var serverStatus = new MinecraftServerStatusManager(server.IpAddress, server.Port);

            //TODO вывод дополнительных свойств типа Motd и пр.
            var serverData = new JsonMinecraftServerData()
            {
                Status = JsonMinecraftServerStatus.Online,
                Message = "Сервер онлайн",
                PlayerCount = serverStatus.GetCurrentPlayers(),
                MaxPlayerCount = serverStatus.GetMaximumPlayers()
            };

            //дополнительная информация о сервере, необходима для лаунчера
            serverData.ServerName = server.Name;
            
            if (!serverStatus.IsServerUp())
            {
                serverData.Status = JsonMinecraftServerStatus.Offline;
                serverData.Message = "Сервер выключен";

                return serverData;
            }

            if (server.ClientVersion != serverStatus.GetVersion())
            {
                serverData.Status = JsonMinecraftServerStatus.Error;
                serverData.Message = "Версия клиента не совпадает с версией сервера";

                return serverData;
            }


            if (serverData.PlayerCount >= serverData.MaxPlayerCount && serverData.MaxPlayerCount != 0)
            {
                serverData.Status = JsonMinecraftServerStatus.Full;
                serverData.Message = "Сервер полон";
            }

            if (serverData.PlayerCount == 0)
            {
                serverData.Status = JsonMinecraftServerStatus.Empty;
                serverData.Message = "Сервер пуст";
            }

            return serverData;
        }
    }
}