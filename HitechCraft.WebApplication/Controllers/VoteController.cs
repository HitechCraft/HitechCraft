namespace HitechCraft.WebApplication.Controllers
{
    using Common.DI;
    using System.Web.Mvc;
    using Common.Core;
    using BL.CQRS.Command;

    public class VoteController : BaseController
    {
        public string TopCraftSecret => "de2d1e56cb36622a79d236bf942a939d";

        public VoteController(IContainer container) : base(container)
        {
        }
        
        public ActionResult VoteOnTopcraft(string timestamp, string username, string signature)
        {
            if (signature.ToLower() == HashManager.GetSha1Hash(username + timestamp + this.TopCraftSecret))
            {
                this.CommandExecutor.Execute(new CurrencyUpdateCommand()
                {
                    Id = this.Currency.Id,
                    Gonts = 250,
                    Rubles = 0
                });

                ViewBag.VoteOn = "Topcraft.ru";
                ViewBag.VoteReward = "250 Gonts на счет";
            }
            else
            {
                ViewBag.VoteError = "Ошибка голосования";
            }

            return View("Index");
        }

        [HttpPost]
        public ActionResult VoteOnMctopsu()
        {
            return View("Index");
        }

        [HttpPost]
        public ActionResult VoteOnMctop()
        {
            return View("Index");
        }
    }
}