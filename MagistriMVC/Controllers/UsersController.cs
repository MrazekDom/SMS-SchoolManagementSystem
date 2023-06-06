using MagistriMVC.Models;
using MagistriMVC.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MagistriMVC.Controllers {
    public class UsersController : Controller {
        private UserManager<AppUser> userManager;       //built in servicka

        public UsersController(UserManager<AppUser> userManager) {
            this.userManager = userManager;
        }

        public IActionResult Index() {
            return View(userManager.Users);
        }
        [HttpGet]
        public IActionResult Create() {
            return View();
        }

        [HttpPost]      //metoda pro vytvoreni usera
        public async Task<IActionResult> Create(UserVM user) {
            if (ModelState.IsValid) {
                AppUser appUser = new AppUser {
                    UserName = user.Name,
                    Email = user.Email
                };
                //pokus o zápis nového uživatele do databáze
                IdentityResult result = await userManager.CreateAsync(appUser, user.Password);
                if (result.Succeeded)
                    return RedirectToAction("Index");
                else {
                    foreach (IdentityError error in result.Errors)
                        ModelState.AddModelError("", error.Description);
                }
            }
            return View(user);
        }
    }
}
