using System;
using System.Security.Principal;
using System.Web;

namespace WebApplication.Controllers
{
    #region Using Directives

    using System.Data.Entity;
    using System.Linq;
    using System.Web.Mvc;
    using Microsoft.AspNet.Identity;
    using Domain;

    #endregion

    public class BaseController : Controller
    {
        public ApplicationDbContext context = new ApplicationDbContext();

        public HttpContextBase HttpContext
        {
            get
            {
                return ControllerContext == null ? null : ControllerContext.HttpContext;
            }
        }

        public IPrincipal User
        {
            get { return this.GetCurrentUser(); }
        }

        private IPrincipal GetCurrentUser()
        {
            if (ViewData["LoggedInUser"] == null)
                ViewData["LoggedInUser"] = this.HttpContext.User;

            return this.HttpContext == null || this.HttpContext.User == null ? (IPrincipal)ViewData["LoggedInUser"] : this.HttpContext.User;
        }

        public string DefaultMaleUserId
        {
            get { return "882155f6-f5a9-4a26-a5dd-d51f58492906"; }
        }

        public string DefaultFemaleUserId
        {
            get { return "f2e20140-3ab1-4b1f-ba30-c03824b3a91b"; }
        }
        
        public double DefaultUserGonts
        {
            get { return 30.00; }
        }

        public double DefaultUserRubels
        {
            get { return 5.00; }
        }

        public Currency UserCurrency
        {
            get
            {
                return this.GetUserCurrency();
            }
        }

        private Currency GetUserCurrency()
        {
            var user = this.User;

            if (user != null && user.Identity.IsAuthenticated)
            {
                var userName = user.Identity.GetUserName().ToString();

                try
                {
                    return this.context.Currencies.First(c => c.username == userName);
                    //TODO: посмотреть где удаляются значения баланса игроков
                }
                catch (Exception)
                {
                    context.Currencies.Add(new Currency
                    {
                        username = userName,
                        balance = this.DefaultUserGonts,
                        realmoney = this.DefaultUserRubels,
                        status = 0
                    });

                    context.SaveChanges();

                    return context.Currencies.First(c => c.username == userName);
                }
            }

            return null;
        }

        public double Gonts
        {
            get { return UserCurrency != null ? UserCurrency.balance : -1; }
        }

        public double Rubles
        {
            get { return UserCurrency != null ? UserCurrency.realmoney : -1; }
        }

        public ApplicationUser CurrentUser {
            get
            {
                if (this.User.Identity.IsAuthenticated)
                {
                    var userId = this.User.Identity.GetUserId();
                    return context.Users.First(c => c.Id == userId);
                }

                return null;
            }
        }

        public Player CurrentPlayer { get; set; }

        public BaseController()
        {
        }
    }
}