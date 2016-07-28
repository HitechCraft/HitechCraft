namespace HitechCraft.WebApplication.Controllers
{
    using System.Web.Mvc;
    using Common.DI;
    using System;
    using BL.CQRS.Command;
    using Common.Core;
    using DAL.Domain;

    public enum IEGroup
    {
        LightPro = 90,
        Pro = 210,
        Vip = 320,
        Premium = 400,
        Ultra = 550
    }

    public class DonateController : BaseController
    {
        public DonateController(IContainer container) : base(container)
        {
        }
        
        public ActionResult Groups()
        {
            return View();
        }
        
        public ActionResult Kits()
        {
            return View();
        }

        #region Server Groups

        public ActionResult GroupsIe()
        {
            return PartialView("_GroupsIE");
        }

        public JsonResult BuyGroupIe(IEGroup group)
        {
            //hash игрока
            var hash = HashManager.UuidFromString("OfflinePlayer:" + this.Player.Name);

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

        #endregion
    }
}