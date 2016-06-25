namespace WebApplication.Domain
{
    using Core;
    using Areas.Launcher.Models.Json;

    public static class ServerExtentions
    {
        public static JsonServerData GetServerData(this Server server)
        {
            var serverStatus = new MinecraftServerStatusManager(server.IpAddress, server.Port);

            if (!serverStatus.IsServerUp())
            {
                return new JsonServerData()
                {
                    Status = JsonServerStatus.Offline,
                    Message = "Сервер выключен"
                };
            }

            if (server.ClientVersion != serverStatus.GetVersion())
            {
                return new JsonServerData()
                {
                    Status = JsonServerStatus.Error,
                    Message = "Версия клиента не совпадает с версией сервера"
                };
            }

            //TODO вывод дополнительных свойств типа Motd и пр.
            var serverData = new JsonServerData()
            {
                Status = JsonServerStatus.Online,
                Message = "Сервер онлайн",
                PlayerCount = serverStatus.GetCurrentPlayers(),
                MaxPlayerCount = serverStatus.GetMaximumPlayers()
            };

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