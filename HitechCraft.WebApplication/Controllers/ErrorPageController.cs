namespace HitechCraft.WebApplication.Controllers
{
    using System.Web.Mvc;

    public class ErrorPageController : Controller
    {
        public ActionResult Error403()
        {
            ViewBag.ErrorCode = "403";
            ViewBag.ErrorMessage = "Доступ закрыт";

            return View("Index");
        }

        public ActionResult Error404()
        {
            ViewBag.ErrorCode = "404";
            ViewBag.ErrorMessage = "Страница не найдена";

            return View("Index");
        }

        public ActionResult Error414()
        {
            ViewBag.ErrorCode = "414";
            ViewBag.ErrorMessage = "Запрос слишком длинный";

            return View("Index");
        }

        public ActionResult Error500()
        {
            ViewBag.ErrorCode = "500";
            ViewBag.ErrorMessage = "Ошибка сервера";

            return View("Index");
        }
    }
}