# .netFramework-identity-with-dapper
Dapper Micro ORM with Asp.Net Identity 2.2.1 in place of Entity Framework

### Declaration
    [Authorize]
    public class AccountController : Controller
    {
        private YourApplicationSignInManager _signInManager;
        private YourApplicationUserManager _userManager;
        ILog logger = LogManager.GetLogger(typeof(AccountController));

        public AccountController()
        {
        }

        public AccountController(YourApplicationUserManager usermanager, YourApplicationSignInManager signinmanager)
        {
            _userManager = usermanager;
            _signInManager = signinmanager;
        }
    
    
### Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await SignInManager.PasswordSignInAsync(
                model.UserName,
                model.Password,
                isPersistent: false,
                shouldLockout: false);

            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToAction("Index", "Home");
                case SignInStatus.LockedOut:
                case SignInStatus.RequiresVerification:
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }
  
  ### Forget Password
          [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<ActionResult> ForgotPassword(ResetPasswordViewModel objResetPassword)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await UserManager.FindByNameAsync(objResetPassword.EmailID);

                    if (user == null)
                    {
                        return View("ForgotPassword");
                    }

                    await SendEmailLinkAsync(user, "Reset Password", "ForgotPasswordTemplate");
                    return RedirectToAction("ForgotPasswordRequestConfirmation");
                }
            }
            catch (Exception ex)
            {
                logger.Error("ForgotPassword: " + ex);
            }

            return View();
        }
        
  ### ChangePasswordForLoggedInUser
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePasswordForLoggedInUser(ChangeLoggedInUserPasswordViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int loggedInUserId = int.Parse(User.Identity.GetUserId());
                    var result = await UserManager.ChangePasswordAsync(loggedInUserId, vm.CurrentPassword, vm.NewPassword);

                    if (result.Succeeded)
                    {
                        SignInManager.AuthenticationManager.SignOut();
                        return RedirectToAction("ChangePasswordConfirmation");

                    }
                    ModelState.AddModelError("ChangePasswordError", "Error updating new password, please ensure you have entered correct current password.");
                    return View();
                }
            }
            catch (Exception ex)
            {
                logger.Error("ChangePasswordForLoggedInUser: " + ex);
            }
            return View(vm);
        }
        
### Logout

        public async Task<ActionResult> Logout()
        {
            try
            {
                SignInManager.AuthenticationManager.SignOut();
                return View("Login");
            }
            catch (Exception ex)
            {
                logger.Error("Logout Get: " + ex);
            }
            return View("SomethingWentWrong");
        }
