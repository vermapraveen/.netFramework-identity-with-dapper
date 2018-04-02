using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using YourApplication.DataLayer;
using YourApplication.Models;

namespace YourApplication.UserIdentity
{
    public class YourApplicationUserManager : UserManager<YourApplicationUser, int>
    {
        public YourApplicationUserManager(IUserStore<YourApplicationUser, int> store)
          : base(store)
        {
        }

        public static YourApplicationUserManager Create(
            IdentityFactoryOptions<YourApplicationUserManager> options,
            IOwinContext context)
        {
            var manager = new YourApplicationUserManager(new YourApplicationUserStore(new YourApplicationUserRepository()));
            manager.UserValidator = new UserValidator<YourApplicationUser, int>(manager)
            {
                AllowOnlyAlphanumericUserNames = true,
                RequireUniqueEmail = true
            };

            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 1,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
            };

            manager.UserLockoutEnabledByDefault = false;

            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(500000);
            manager.MaxFailedAccessAttemptsBeforeLockout = 500000;

            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider =
                                    new DataProtectorTokenProvider<YourApplicationUser, int>(dataProtectionProvider.Create("ASP.NET Identity"))
                                    {
                                        TokenLifespan = TimeSpan.FromHours(48)
                                    };
            }

            // manager.EmailService = new EmailService();
            return manager;
        }
    }
}
