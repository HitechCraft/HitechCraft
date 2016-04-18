namespace WebApplication.Domain
{
    #region Usings

    using System.ComponentModel.DataAnnotations;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    #endregion

    public enum Gender
    {
        [Display(Name = "GenderMale", ResourceType = typeof(Resources))]
        Male,
        [Display(Name = "GenderFemale", ResourceType = typeof(Resources))]
        Female
    }

    public class ApplicationUser : IdentityUser
    {
        public Gender Gender { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }
    }
}