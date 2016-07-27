namespace HitechCraft.WebApplication.Controllers
{
    using Common.DI;
    using System.Web.Mvc;
    using Common.Core;
    using BL.CQRS.Command;
    using System;
    using System.Collections.Generic;

    public class VoteController : BaseController
    {
        public string TopCraftSecret => "de2d1e56cb36622a79d236bf942a939d";

        public string MCTopSecret => "h5h43hrehfdse7we2rujdjfvz";

        public string MCTopSuSecret => "3j4j34jfdsf9e932kjfdksjgdssepe3l2k";

        public VoteController(IContainer container) : base(container)
        {
        }
        
        [HttpPost]
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

        [HttpGet]
        public ActionResult VoteOnMctopsu(string nickname, string token)
        {
            if (token == HashManager.GetMd5Hash(nickname + this.MCTopSuSecret))
            {
                this.CommandExecutor.Execute(new CurrencyUpdateCommand()
                {
                    Id = this.Currency.Id,
                    Gonts = 0,
                    Rubles = 2
                });

                ViewBag.VoteOn = "McTop.su";
                ViewBag.VoteReward = "2 RUB на счет";
            }
            else
            {
                ViewBag.VoteError = "Ошибка голосования";
            }

            return View("Index");
        }

        [HttpGet]
        public ActionResult VoteOnMctop(string username, string test, string source, string sign)
        {
            List<string> objects = new List<string>()
            {
                username,
                test,
                source
            };

            if (sign == this.McTopSignBuilder(objects))
            {
                //TODO: сделать выдачу приза (товар из магазина ресурсов)

                ViewBag.VoteOn = "McTop";
                ViewBag.VoteReward = "[наименование вещи]";
            }
            else
            {
                ViewBag.VoteError = "Ошибка голосования";
            }

            return View("Index");
        }
        
        #region Private MEthods

        private string McTopSignBuilder(List<string> objects)
        {
            var paramString = this.Implode(objects, "");

            var md5 = HashManager.GetMd5Hash(paramString + this.MCTopSecret);

            return md5;
        }

        private string Implode(List<string> objects, string separator)
        {
            return String.Join(separator, objects);
        }

        #endregion
    }
}