namespace WebApplication.Controllers
{
    using Areas.Launcher.Models.Json;
    using Domain;
    using System.Web.Mvc;

    public class ServerController : BaseController
    {
        public JsonResult GetServerData(string ipAddress, int port)
        {
            var serverData = new Server()
            {
                IpAddress = ipAddress,
                Port = port
            }.GetServerData();

            return Json(new JsonServerData());
        }
    }
}
