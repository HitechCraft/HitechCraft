using HitechCraft.Common.DI;

namespace HitechCraft.DAL.Domain.Extentions
{
    #region Using Directives

    using Common.Models.Json.MinecraftServer;
    using Common.Core;

    #endregion
    
    public static class ServerExtentions
    {
        /// <summary>
        /// Getting info from server
        /// </summary>
        /// <param name="server"></param>
        /// <param name="image"></param>
        /// <returns></returns>
        public static JsonMinecraftServerData GetServerData(this Server server, byte[] image)
        {
            var serverStatus = new MinecraftServerManager(server.IpAddress, server.Port);
            
            //TODO вывод дополнительных свойств типа Motd и пр.
            var serverData = new JsonMinecraftServerData()
            {
                Status = JsonMinecraftServerStatus.Online,
                Message = "Сервер онлайн",
                PlayerCount = serverStatus.GetCurrentPlayers(),
                MaxPlayerCount = serverStatus.GetMaximumPlayers(),
                Image = image
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