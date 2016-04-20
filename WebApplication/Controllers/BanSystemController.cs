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

            return View(banLogs);
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

                return View(banActionView);
            }
            catch (Exception)
            {
                new HttpNotFoundResult();
            }

            return View();
        }
    }
}
