using HitechCraft.Core.DI;

namespace HitechCraft.WebApplication.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;

    [Authorize(Roles = "Administrator")]
    public class AdministrationController : BaseController
    {
        private ApplicationDbContext _context;
        private ApplicationUserManager _userManager;
        private RoleManager<IdentityRole> _roleManager;
        
        public AdministrationController(IContainer container) : base(container)
        {
            this._context = new ApplicationDbContext();
            var store = new UserStore<ApplicationUser>(this._context);

            this._userManager = new ApplicationUserManager(store);
            this._roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(this._context));
        }

        // GET: Administration
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AddUserToRole()
        {
            ViewBag.RoleList = this._context.Roles.Where(x => x.Name != "User").Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id
            });

            return View();
        }

        [HttpPost]
        public ActionResult AddUserToRole(UserRoleViewModel userRole)
        {
            if(String.IsNullOrEmpty(userRole.UserName)) ModelState.AddModelError(string.Empty, "Введите имя пользователя");

            if (ModelState.IsValid)
            {
                var user = this._userManager.FindByNameAsync(userRole.UserName);

                if(user.Result == null) ModelState.AddModelError(string.Empty, "Пользователя не существует");
                else
                    if (!this._userManager.IsInRole(user.Result.Id, this._roleManager.Roles.First(x => x.Id == userRole.RoleId).Name))
                    {
                        try
                        {
                            this._userManager.AddToRole(user.Result.Id,
                                this._roleManager.Roles.First(x => x.Id == userRole.RoleId).Name);

                            return RedirectToAction("AddUserToRole");
                        }
                        catch (Exception e)
                        {
                            ModelState.AddModelError(string.Empty, e.Message);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Пользователь уже имеет такую роль");
                    }
            }

            ViewBag.RoleList = this._context.Roles.Where(x => x.Name != "User").Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id
            });

            return View(userRole);
        }
        
        [HttpPost]
        public JsonResult AddUserRole(RoleViewModel role)
        {
            if (ModelState.IsValid)
            {
                if (!this._context.Roles.Any(r => r.Name == role.Name))
                {
                    try
                    {
                        this._roleManager.Create(new IdentityRole(role.Name));
                    }
                    catch (Exception e)
                    {
                        return Json(new { status = "NO", message = e.Message });
                    }
                }
                else
                {
                    return Json(new { status = "NO", message = "Роль с таким именем уже существует" });
                }
            }
            
            return Json(new { status = "OK", message = "Роль успешно добавлена" });
        }
    }
}