using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using System.Security.Claims;
using System.Threading.Tasks;
using YourApplication.Models;

namespace YourApplication.UserIdentity
{
    public class YourApplicationSignInManager : SignInManager<YourApplicationUser, int>
    {
        public YourApplicationSignInManager(YourApplicationUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(YourApplicationUser user)
        {
            return user.GenerateUserIdentityAsync((YourApplicationUserManager)UserManager);
        }

        public static YourApplicationSignInManager Create(IdentityFactoryOptions<YourApplicationSignInManager> options, IOwinContext context)
        {
            return new YourApplicationSignInManager(context.GetUserManager<YourApplicationUserManager>(), context.Authentication);
        }
    }
}
