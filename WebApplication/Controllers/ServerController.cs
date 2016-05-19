namespace WebApplication.Controllers
{
    using Areas.Launcher.Models.Json;
    using Core;
    using Domain;
    using System.Web.Mvc;
    using System.Linq;

    public class ServerController : BaseController
    {
        public JsonResult GetServerData(string ipAddress, int? port)
        {
            try
            {
                var server = this.context.Servers.First(s => s.IpAddress == ipAddress && s.Port == (port != null ? port : 25565).Value);

                var serverStatus = new MinecraftServerStatusManager(server.IpAddress, server.Port);

                if (server.ClientVersion != serverStatus.GetVersion())
                {
                    return Json(new JsonServerData()
                    {
                        Status = JsonServerStatus.Error,
                        Message = "Версия клиента не совпадает с версией сервера"
                    },
                    JsonRequestBehavior.AllowGet);
                }

                if (!serverStatus.IsServerUp())
                {
                    return Json(new JsonServerData()
                    {
                        Status = JsonServerStatus.Offline,
                        Message = "Сервер выключен"
                    },
                    JsonRequestBehavior.AllowGet);
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

                if(serverData.PlayerCount == 0)
                {
                    serverData.Status = JsonServerStatus.Empty;
                    serverData.Message = "Сервер пуст";
                }

                if(serverData.PlayerCount >= serverData.MaxPlayerCount)
                {
                    serverData.Status = JsonServerStatus.Full;
                    serverData.Message = "Сервер полон";
                }

                return Json(serverData, JsonRequestBehavior.AllowGet);
            }
            catch (System.Exception)
            {
                return Json(new JsonServerData()
                {
                    Status = JsonServerStatus.Error,
                    Message = "Указанный сервер не найден"
                },
                JsonRequestBehavior.AllowGet);
            }
        }
    }
}
