namespace HitechCraft.WebApplication.Controllers
{
    #region Using Directives

    using System.Web.Mvc;
    using System;
    using BL.CQRS.Command;
    using System.Linq;
    using BL.CQRS.Query;
    using Models;
    using HitechCraft.Core.Databases;
    using HitechCraft.Core.DI;
    using HitechCraft.Core.Entity;
    using HitechCraft.Core.Helper;
    using HitechCraft.Core.Repository.Specification.Permissions;
    using HitechCraft.Projector.Impl;

    #endregion

    public enum IEGroup
    {
        LightPro = 90,
        Pro = 210,
        Vip = 320,
        Premium = 400,
        Ultra = 620
    }

    public class DonateController : BaseController
    {
        public DonateController(IContainer container) : base(container)
        {
        }

        [Authorize]
        public ActionResult Groups()
        {
            return View();
        }

        [Authorize]
        public ActionResult Kits()
        {
            return View();
        }

        #region Server Groups

        public ActionResult GroupsIe()
        {
            ViewBag.NickName = this.Player != null ? this.Player.Name : "NickName";
            
            ViewBag.GroupStatus = this.CheckAvailableGroups();

            return PartialView("_GroupsIE");
        }

        //TODO: сделать дополнительную проверку о покупке (есть ли активные группы), во избежании внедрения html
        public JsonResult BuyGroupIe(IEGroup group)
        {
            //hash игрока
            var hash = HashHelper.UuidFromString("OfflinePlayer:" + this.Player.Name);

            //Так конечно нельзя, но да пофигу
            var permissions = new Permissions()
            {
                Name = hash,
                Type = 1
            };
            
            var pexInheritance = new PexInheritance()
            {
                Type = 1,
                Child = hash
            };

            //имя группы в pex
            var groupString = String.Empty;

            try
            {
                switch (group)
                {
                    case IEGroup.LightPro:
                        groupString = "lightpro";
                        break;
                    case IEGroup.Pro:
                        groupString = "pro";
                        break;
                    case IEGroup.Vip:
                        groupString = "vip";
                        break;
                    case IEGroup.Premium:
                        groupString = "premium";
                        break;
                    case IEGroup.Ultra:
                        groupString = "ultra";
                        break;
                    default:
                        throw new Exception("Группа не найдена");
                }

                permissions.Permission = "group-" + groupString + "-until";
                pexInheritance.Parent = groupString;

                this.CommandExecutor.Execute(new DonateGroupIEBuyCommand()
                {
                    CurrencyId = this.Currency.Id,
                    PlayerName = this.Player.Name,
                    Permissions = permissions,
                    PexInheritance = pexInheritance,
                    Price = (int)group
                });
            }
            catch (Exception e)
            {
                return Json(new { status = "NO", message = "Группа не приобретена. " + e.Message });
            }

            return Json(new { status = "OK", message = "Группа успешно приоретена!" });
        }

        public string CheckAvailableGroups()
        {
            try
            {
                var group = new BL.CQRS.Query.Specify.EntityListQueryHandler<Permissions, PermissionsViewModel, IEConnection>(this.Container)
                    .Handle(new EntityListQuery<Permissions, PermissionsViewModel>()
                    {
                        Projector = this.Container.Resolve<IProjector<Permissions, PermissionsViewModel>>(),
                        Specification =
                        new PermissionsByNameSpec(HashHelper.UuidFromString("OfflinePlayer:" + this.Player.Name)) & new PermissionsByGroupContainsSpec()
                    }).First();

                var datetime = new DateTime(1970, 1, 1).AddSeconds(int.Parse(group.Value));

                return "Вы уже приобрели группу на данном сервере! У вас активная группа до " + datetime + ".";
            }
            catch (Exception e)
            {
                return String.Empty;
            }
        }

        #endregion
    }
}