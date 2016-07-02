namespace WebApplication.Domain
{
    using Core;
    using Areas.Launcher.Models.Json;

    public static class ServerExtentions
    {
        public static JsonServerData GetServerData(this Server server)
        {
            var serverStatus = new MinecraftServerStatusManager(server.IpAddress, server.Port);

            //TODO вывод дополнительных свойств типа Motd и пр.
            var serverData = new JsonServerData()
            {
                Status = JsonServerStatus.Online,
                Message = "Сервер онлайн",
                PlayerCount = serverStatus.GetCurrentPlayers(),
                MaxPlayerCount = serverStatus.GetMaximumPlayers()
            };

            //дополнительная информация о сервере, необходима для лаунчера
            serverData.ServerName = server.Name;
            
            if (!serverStatus.IsServerUp())
            {
                serverData.Status = JsonServerStatus.Offline;
                serverData.Message = "Сервер выключен";

                return serverData;
            }

            if (server.ClientVersion != serverStatus.GetVersion())
            {
                serverData.Status = JsonServerStatus.Error;
                serverData.Message = "Версия клиента не совпадает с версией сервера";

                return serverData;
            }


            if (serverData.PlayerCount >= serverData.MaxPlayerCount && serverData.MaxPlayerCount != 0)
            {
                serverData.Status = JsonServerStatus.Full;
                serverData.Message = "Сервер полон";
            }

            if (serverData.PlayerCount == 0)
            {
                serverData.Status = JsonServerStatus.Empty;
                serverData.Message = "Сервер пуст";
            }

            return serverData;
        }
    }
}