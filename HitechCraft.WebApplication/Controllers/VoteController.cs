using HitechCraft.Core.DI;
using HitechCraft.Core.Entity;
using HitechCraft.Core.Helper;
using HitechCraft.Core.Repository.Specification.Currency;
using HitechCraft.Projector.Impl;

namespace HitechCraft.WebApplication.Controllers
{
    #region Using Directives

    using System.Web.Mvc;
    using BL.CQRS.Command;
    using System;
    using System.Collections.Generic;
    using BL.CQRS.Query;
    using Models;
    using System.Linq;

    #endregion

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
            if (signature.ToLower() == HashHelper.GetSha1Hash(username + timestamp + this.TopCraftSecret))
            {
                try
                {
                    this.PayVoteGonts(username);

                    ViewBag.VoteOn = "Topcraft.ru";
                    ViewBag.VoteReward = "250 Gonts на счет";

                    return this.Content("OK");
                }
                catch (Exception e)
                {
                    ViewBag.VoteError = "Ошибка голосования";

                    LogHelper.Error(username + ": Ошибка голосования. " + e.Message);
                }
            }
            else
            {
                ViewBag.VoteError = "Ошибка голосования";
            }

            return this.Content("NO");
        }

        [HttpGet]
        public ActionResult VoteOnMctopsu(string nickname, string token)
        {
            if (token == HashHelper.GetMd5Hash(nickname + this.MCTopSuSecret))
            {
                try
                {
                    this.PayVoteRubles(nickname);

                    ViewBag.VoteOn = "McTop.su";
                    ViewBag.VoteReward = "2 RUB на счет";

                    return this.Content("OK");
                }
                catch (Exception e)
                {
                    ViewBag.VoteError = "Ошибка голосования";

                    LogHelper.Error(nickname + ": Ошибка голосования. " + e.Message);
                }
            }
            else
            {
                ViewBag.VoteError = "Ошибка голосования";
            }

            return this.Content("NO");
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
                try
                {
                    this.GetVoteShopItem(username);
                }
                catch (Exception e)
                {
                    ViewBag.VoteError = "Ошибка голосования";

                    LogHelper.Error(username + ": Ошибка голосования. " + e.Message);
                }
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

            var md5 = HashHelper.GetMd5Hash(paramString + this.MCTopSecret);

            return md5;
        }

        private string Implode(List<string> objects, string separator)
        {
            return String.Join(separator, objects);
        }

        private void PayVoteGonts(string userName)
        {
            var currency = new EntityListQueryHandler<Currency, CurrencyEditViewModel>(this.Container)
                .Handle(new EntityListQuery<Currency, CurrencyEditViewModel>()
                {
                    Specification = new CurrencyByPlayerNameSpec(userName),
                    Projector = this.Container.Resolve<IProjector<Currency, CurrencyEditViewModel>>()
                }).First();

            this.CommandExecutor.Execute(new CurrencyUpdateCommand()
            {
                Id = currency.Id,
                Gonts = 250,
                Rubles = 0
            });

            LogHelper.Info(currency.PlayerName + ": голосование на Topcraft. Награда - 250 Gonts");
        }

        private void PayVoteRubles(string userName)
        {
            var currency = new EntityListQueryHandler<Currency, CurrencyEditViewModel>(this.Container)
                        .Handle(new EntityListQuery<Currency, CurrencyEditViewModel>()
                        {
                            Specification = new CurrencyByPlayerNameSpec(userName),
                            Projector = this.Container.Resolve<IProjector<Currency, CurrencyEditViewModel>>()
                        }).First();

            this.CommandExecutor.Execute(new CurrencyUpdateCommand()
            {
                Id = currency.Id,
                Gonts = 0,
                Rubles = 2
            });

            LogHelper.Info(currency.PlayerName + ": голосование на MCtopSU. Награда - 2 RUB");
        }

        private void GetVoteShopItem(string userName)
        {
            this.CommandExecutor.Execute(new ShopItemAddRandomCommand()
            {
                PlayerName = userName
            });

            LogHelper.Info(userName + ": голосование на MCtop. Награда - случайный предмет из магазина");
        }

        #endregion
    }
}