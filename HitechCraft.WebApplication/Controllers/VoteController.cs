using HitechCraft.Core;
using HitechCraft.WebApplication.Manager;

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
    using Core.DI;
    using Core.Entity;
    using Core.Helper;
    using Core.Repository.Specification.Currency;
    using Projector.Impl;

    #endregion

    public class VoteController : BaseController
    {
        public VoteController(IContainer container) : base(container)
        {
        }

        [HttpPost]
        public ActionResult VoteOnTopcraft(string timestamp, string username, string signature)
        {
            if (signature.ToLower() == HashHelper.GetSha1Hash(username + timestamp + Const.TopCraftKey))
            {
                try
                {
                    PayVoteGonts(username);

                    ViewBag.VoteOn = "Topcraft.ru";
                    ViewBag.VoteReward = "250 Gonts на счет";

                    return Content("OK");
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

            return Content("NO");
        }

        [HttpGet]
        public ActionResult VoteOnMctopsu(string nickname, string token)
        {
            if (token == HashHelper.GetMd5Hash(nickname + Const.McTopSuKey))
            {
                try
                {
                    PayVoteGonts(nickname);

                    ViewBag.VoteOn = "McTop.su";
                    ViewBag.VoteReward = "250 Gonts на счет";

                    return Content("OK");
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

            return Content("NO");
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

            if (sign == McTopSignBuilder(objects))
            {
                try
                {
                    PayVoteGonts(username);
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
            var paramString = Implode(objects, "");

            var md5 = HashHelper.GetMd5Hash(paramString + Const.McTopKey);

            return md5;
        }

        private string Implode(List<string> objects, string separator)
        {
            return String.Join(separator, objects);
        }

        private void PayVoteGonts(string userName)
        {
            var currency = new EntityListQueryHandler<Currency, CurrencyEditViewModel>(Container)
                .Handle(new EntityListQuery<Currency, CurrencyEditViewModel>()
                {
                    Specification = new CurrencyByPlayerNameSpec(userName),
                    Projector = Container.Resolve<IProjector<Currency, CurrencyEditViewModel>>()
                }).First();

            CommandExecutor.Execute(new CurrencyUpdateCommand()
            {
                Id = currency.Id,
                Gonts = 100,
                Rubles = 0
            });

            LogHelper.Info(currency.PlayerName + ": Награда - 100 Gonts");
        }

        private void PayVoteRubles(string userName)
        {
            var currency = new EntityListQueryHandler<Currency, CurrencyEditViewModel>(Container)
                        .Handle(new EntityListQuery<Currency, CurrencyEditViewModel>()
                        {
                            Specification = new CurrencyByPlayerNameSpec(userName),
                            Projector = Container.Resolve<IProjector<Currency, CurrencyEditViewModel>>()
                        }).First();

            CommandExecutor.Execute(new CurrencyUpdateCommand()
            {
                Id = currency.Id,
                Gonts = 0,
                Rubles = 2
            });

            LogHelper.Info(currency.PlayerName + ": голосование на MCtopSU. Награда - 2 RUB");
        }

        private void GetVoteShopItem(string userName)
        {
            CommandExecutor.Execute(new ShopItemAddRandomCommand()
            {
                PlayerName = userName
            });

            LogHelper.Info(userName + ": голосование на MCtop. Награда - случайный предмет из магазина");
        }

        #endregion
    }
}