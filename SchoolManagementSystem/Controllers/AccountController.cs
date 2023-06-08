using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystem.Models;
using SchoolManagementSystem.ViewModels;

namespace SchoolManagementSystem.Controllers {
    [Authorize]
    public class AccountController : Controller {
        private UserManager<AppUser> _userManager;
        private SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager) {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [HttpGet]
        [AllowAnonymous]            //dovoli prihlasovani
        public IActionResult Login(string returnUrl) {
            LoginVM loginVM = new LoginVM();
            loginVM.returnUrl = returnUrl;
            return View(loginVM);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]          //dovoli prihlasovani
        public async Task<IActionResult> Login(LoginVM login) {
            if (ModelState.IsValid) {
                AppUser appUser = await _userManager.FindByNameAsync(login.UserName);
                if (appUser != null) {
                    await _signInManager.SignOutAsync();
                    Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(appUser,
                    login.Password, login.Remember, false);     //persistent cookie
                    if (result.Succeeded) {
                        return Redirect(login.returnUrl ?? "/");
                    }
                }
                ModelState.AddModelError(nameof(login.UserName), "Login Failed: Invalid User Name or Password");
            }
            return View(login);
        }
        public async Task<IActionResult> LogOut() {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }



    }
}
