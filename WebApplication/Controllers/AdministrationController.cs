using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class AdministrationController : BaseController
    {
        // GET: Administration
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AddUserRole()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddUserRole(RoleViewModel role)
        {
            if (ModelState.IsValid)
            {
                if (!this.context.Roles.Any(r => r.Name == role.Name))
                {
                    try
                    {
                        var RoleManager =
                            new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
                        RoleManager.Create(new IdentityRole(role.Name));

                        return RedirectToAction("AddUserRole");
                    }
                    catch (Exception e)
                    {
                        ModelState.AddModelError(string.Empty, e.Message);
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, Resources.Administration_AddRole_ErrorAlreadyExists);
                }
            }

            return View(role);
        }

    }
}