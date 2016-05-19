namespace WebApplication.Domain
{
    using Core;
    using Areas.Launcher.Models.Json;

    public static class ServerExtentions
    {
        public static JsonServerData GetServerData(this Server server)
        {
            var serverStatus = new MinecraftServerStatusManager(server.IpAddress, server.Port);

            if (server.ClientVersion != serverStatus.GetVersion())
            {
                new JsonServerData()
                {
                    Status = JsonServerStatus.Error,
                    Message = "Версия клиента не совпадает с версией сервера"
                };
            }

            if (!serverStatus.IsServerUp())
            {
                new JsonServerData()
                {
                    Status = JsonServerStatus.Offline,
                    Message = "Сервер выключен"
                };
            }

            //TODO автомаппер прикрутить потом
            var serverData = new JsonServerData()
            {
                Status = JsonServerStatus.Online,
                Message = "Сервер онлайн",
                ServerName = server.Name,
                ServerDescription = server.Description,
                IpAddress = server.IpAddress,
                Port = server.Port,
                ClientVersion = server.ClientVersion,
                PlayerCount = serverStatus.GetCurrentPlayers(),
                MaxPlayerCount = serverStatus.GetMaximumPlayers()
            };

            if (serverData.PlayerCount == 0)
            {
                serverData.Status = JsonServerStatus.Empty;
                serverData.Message = "Сервер пуст";
            }

            if (serverData.PlayerCount >= serverData.MaxPlayerCount)
            {
                serverData.Status = JsonServerStatus.Full;
                serverData.Message = "Сервер полон";
            }

            return serverData;
        }
    }
}