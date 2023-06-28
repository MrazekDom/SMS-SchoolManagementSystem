using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SMS.Models.Models;
using SMS.Models.ViewModels;

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
        [AllowAnonymous]            //uzivatel je presmerovan sem,dovoli prihlasovani
        public IActionResult Login(string returnUrl) {      //z home controlleru ktery ma anotaci Authorize jsem poslan sem, parametr predavam returnUrl, ktery mam stored
            LoginVM loginVM = new LoginVM();
            loginVM.returnUrl = returnUrl;
            return View(loginVM);   //zobrazi View pro prihlaseni 
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
                    login.Password, login.Remember, false);
                    if (result.Succeeded) {
                        return Redirect(login.returnUrl ?? "/");    //jestlize se prihlaseni povede, tak me to redirectne na returnUrl, ktery je "index" akce od HomeControlleru
                    }
                }
                ModelState.AddModelError(nameof(login.UserName), "Login Failed: Invalid User Name or Password");
            }
            return View(login);
        }
        public async Task<IActionResult> LogOut() {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");   //redirect na "index" akci od "HomeControlleru", uz jsem ale odhlasen, takze to uzivatele zpatky vyhodi na "login" akci co je tady
        }

        public IActionResult AccessDenied() {
            return View();
        }




    }
}
