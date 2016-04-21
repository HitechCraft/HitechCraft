﻿using System;

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
        public ActionResult CreateAction(BanViewModel vm)
        {
            if (vm.PlayerName == String.Empty)
            {
                ModelState.AddModelError(string.Empty, "Необходимо выбрать игрока");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    vm.BannedBy = User.Identity.Name;
                    vm.ActionTime = DateTime.Now;

                    Mapper.CreateMap<Ban, BanEditViewModel>()
                        .ForMember(dst => dst.PlayerName, exp => exp.MapFrom(src => src.name))
                        .ForMember(dst => dst.ActionTime, exp => exp.MapFrom(src => src.time))
                        .ForMember(dst => dst.BannedBy, exp => exp.MapFrom(src => src.admin))
                        .ForMember(dst => dst.Reason, exp => exp.MapFrom(src => src.reason))
                        .ForMember(dst => dst.TempTime, exp => exp.MapFrom(src => src.temptime))
                        .ForMember(dst => dst.Type, exp => exp.MapFrom(src => (BanType)src.type));

                    var banAction = Mapper.Map<BanViewModel, Ban>(vm);

                }
                catch (Exception e)
                {
                    
                    throw;
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
    }
}
