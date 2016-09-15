namespace HitechCraft.WebApplication.Ninject.Current
{
    using System.Web;
    using Microsoft.AspNet.Identity;

    public class CurrentUser : ICurrentUser
    {
        public string Login { get; set; }

        public CurrentUser()
        {
            this.Login = this.GetLogin();
        }

        string GetLogin()
        {
            return HttpContext.Current.User.Identity.GetUserName();
        }
    }
}