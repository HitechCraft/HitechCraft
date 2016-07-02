using System;
using System.Data.Entity;
using System.Net;
using System.Web.Security;

namespace WebApplication.Controllers
{
    #region Using Derectives

    using System.Collections.Generic;
    using Domain;
    using Models;
    using System.Linq;
    using AutoMapper;
    using System.Web.Mvc;

    #endregion

    [Authorize(Roles = "Moderator, Administrator")]
    public class BanSystemController : BaseController
    {
        public ActionResult Index()
        {
            ViewBag.Player = context.Users.Select(u => new SelectListItem
            {
                Text = u.UserName,
                Value = u.Id
            });
            
            return View();
        }
        
        public ActionResult BanListPartial(int? banType, string playerId = "")
        {
            var bans = context.Bans.ToList();

            Mapper.CreateMap<Ban, BanViewModel>()
                    .ForMember(dst => dst.Id, exp => exp.MapFrom(src => src.id))
                    .ForMember(dst => dst.PlayerName, exp => exp.MapFrom(src => src.name))
                    .ForMember(dst => dst.ActionTime, exp => exp.MapFrom(src =>
                    new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(src.time).ToLocalTime()))
                    .ForMember(dst => dst.BannedBy, exp => exp.MapFrom(src => src.admin))
                    .ForMember(dst => dst.Reason, exp => exp.MapFrom(src => src.reason))
                    .ForMember(dst => dst.TempTime, exp => exp.MapFrom(src =>
                    new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(src.temptime).ToLocalTime()))
                    .ForMember(dst => dst.Type, exp => exp.MapFrom(src => (BanType)src.type));

            IEnumerable<BanViewModel> banLogs = Mapper.Map<IEnumerable<Ban>, IEnumerable<BanViewModel>>(bans);

            //фильтрация записей

            if (banType != null)
            {
                banLogs = banLogs.Where(bl => bl.Type == (BanType)banType);
            }

            if (playerId != String.Empty)
            {
                banLogs = banLogs.Where(bl => bl.PlayerName == context.Users.First(u => u.Id == playerId).UserName);
            }

            return PartialView(banLogs);
        }
        
        public ActionResult BanAction(int actionId)
        {
            try
            {
                var banAction = context.Bans.Find(actionId);

                Mapper.CreateMap<Ban, BanViewModel>()
                    .ForMember(dst => dst.Id, exp => exp.MapFrom(src => src.id))
                    .ForMember(dst => dst.PlayerName, exp => exp.MapFrom(src => src.name))
                    .ForMember(dst => dst.ActionTime, exp => exp.MapFrom(src =>
                    new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(src.time).ToLocalTime()))
                    .ForMember(dst => dst.BannedBy, exp => exp.MapFrom(src => src.admin))
                    .ForMember(dst => dst.Reason, exp => exp.MapFrom(src => src.reason))
                    .ForMember(dst => dst.TempTime, exp => exp.MapFrom(src =>
                    new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(src.temptime).ToLocalTime()))
                    .ForMember(dst => dst.Type, exp => exp.MapFrom(src => (BanType)src.type));

                BanViewModel banActionView = Mapper.Map<Ban, BanViewModel>(banAction);

                ViewBag.PlayerId = context.Users.First(u => u.UserName == banActionView.PlayerName).Id;

                return View(banActionView);
            }
            catch (Exception)
            {
                new HttpNotFoundResult();
            }

            return View();
        }

        public ActionResult PlayerBanList(string playerName = "")
        {
            if(playerName != String.Empty) ViewBag.PlayerId = context.Users.First(u => u.UserName == playerName).Id;

            return View();
        }
        
        [HttpGet]
        public ActionResult CreateAction()
        {
            ViewBag.PlayerName = context.Users.Select(u => new SelectListItem
            {
                Text = u.UserName,
                Value = u.UserName
            });

            ViewBag.BannedBy = context.Users.Select(u => new SelectListItem
            {
                Text = u.UserName,
                Value = u.UserName
            });

            return View();
        }

        [HttpPost]
        public ActionResult CreateAction(BanEditViewModel vm)
        {
            int unix = 0;

            if (vm.PlayerName == String.Empty)
            {
                ModelState.AddModelError(string.Empty, "Необходимо выбрать игрока");
            }

            if (vm.TempTime != null)
            {
                unix = (int)vm.TempTime.Value.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;

                if (vm.IsTemped && unix < 0)
                {
                    ModelState.AddModelError("TempTime", "Необходимо выбрать дату");
                }
            }

            if (ModelState.IsValid)
            {
                try
                {
                    vm.ActionTime = DateTime.Now;
                    vm.BannedBy = this.User.Identity.Name;

                    Mapper.CreateMap<BanEditViewModel, Ban>()
                        .ForMember(dst => dst.name, exp => exp.MapFrom(src => src.PlayerName))
                        .ForMember(dst => dst.time, exp => exp.MapFrom(src => (int)src.ActionTime.Subtract(new DateTime(1970, 1, 1)).TotalSeconds))
                        .ForMember(dst => dst.admin, exp => exp.MapFrom(src => src.BannedBy))
                        .ForMember(dst => dst.reason, exp => exp.MapFrom(src => src.Reason))
                        .ForMember(dst => dst.temptime, exp => exp.MapFrom(src => src.TempTime != null ? unix : 0))
                        .ForMember(dst => dst.type, exp => exp.MapFrom(src => src.Type));

                    var banAction = Mapper.Map<BanEditViewModel, Ban>(vm);

                    if (!IsBanExists(banAction))
                    {
                        context.Bans.Add(banAction);
                        context.SaveChanges();
                    }
                    else
                    {
                        ModelState.AddModelError(String.Empty, "Такое наказание у пользователя все еще действует");
                    }
                }
                catch (Exception e)
                {
                    ModelState.AddModelError(String.Empty, e.Message);
                }
            }

            ViewBag.PlayerName = context.Users.Select(u => new SelectListItem
            {
                Text = u.UserName,
                Value = u.UserName
            });

            ViewBag.BannedBy = context.Users.Select(u => new SelectListItem
            {
                Text = u.UserName,
                Value = u.UserName
            });

            return View(vm);
        }

        private bool IsBanExists(Ban ban)
        {
            try
            {
                var banAction = context.Bans.First(b => b.name == ban.name && b.type == ban.type);

                if (banAction.temptime != 0 && banAction.temptime < (int)DateTime.Now.Subtract(new DateTime(1970, 1, 1)).TotalSeconds)
                {
                    return false;
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public ActionResult Unban(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var banAction = context.Bans.Find(id);

            if (banAction.type == 0 || banAction.type == 6)
            {

                if (banAction == null)
                {
                    return HttpNotFound();
                }

                Mapper.CreateMap<Ban, BanUnbanViewModel>()
                    .ForMember(dst => dst.Id, exp => exp.MapFrom(src => src.id))
                    .ForMember(dst => dst.PlayerName, exp => exp.MapFrom(src => src.name))
                    .ForMember(dst => dst.ActionTime, exp => exp.MapFrom(src =>
                    new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(src.time).ToLocalTime()))
                    .ForMember(dst => dst.Type, exp => exp.MapFrom(src => (BanType)src.type));

                var vm = Mapper.Map<Ban, BanUnbanViewModel>(banAction);

                return View(vm);
            }
            else
            {
                ViewBag.TypeError = "Игрока можно только разбанить или освободить из тюрьмы";

                return View();
            }
        }

        [HttpPost, ActionName("Unban")]
        [ValidateAntiForgeryToken]
        public ActionResult UnbanConfirmed(int id)
        {
            var banAction = context.Bans.Find(id);

            switch (banAction.type)
            {
                case 0:
                    banAction.reason = "Unbanned: ";
                    banAction.type = 5;
                    break;
                case 6:
                    banAction.reason = "Released From Jail";
                    banAction.type = 8;
                    break;
            }

            context.Entry(banAction).State = EntityState.Modified;
            context.SaveChanges();
            
            return RedirectToAction("Index");
        }
    }
}
