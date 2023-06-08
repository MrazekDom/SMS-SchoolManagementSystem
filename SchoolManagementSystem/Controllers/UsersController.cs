using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystem.Models;
using SchoolManagementSystem.ViewModels;

namespace SchoolManagementSystem.Controllers {
    [Authorize]
    public class UsersController : Controller {
        private UserManager<AppUser> userManager;       //built in servicka
        private IPasswordHasher<AppUser> passwordHasher;        //pro zahasovani a overeni hesla v databazi

        public UsersController(UserManager<AppUser> userManager, IPasswordHasher<AppUser> passwordHasher) {
            this.userManager = userManager;
            this.passwordHasher = passwordHasher;
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
        [HttpGet]
        public async Task<IActionResult> Edit(string id) {
            AppUser userToEdit = await userManager.FindByIdAsync(id);
            if (userToEdit == null)
                return View("NotFound");
            else
                return View(userToEdit);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, string email, string password) {
            AppUser user = await userManager.FindByIdAsync(id);
            if (user != null) {
                if (!string.IsNullOrEmpty(email))
                    user.Email = email;
                else
                    ModelState.AddModelError("", "Email cannot be empty");
                if (!string.IsNullOrEmpty(password))
                    user.PasswordHash = passwordHasher.HashPassword(user, password);
                else
                    ModelState.AddModelError("", "Password cannot be empty");
                if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password)) {
                    IdentityResult result = await userManager.UpdateAsync(user);
                    if (result.Succeeded)
                        return RedirectToAction("Index");
                    else
                        Errors(result);
                }
            }
            else
                ModelState.AddModelError("", "User Not Found");
            return View(user);
        }

        private void Errors(IdentityResult result) {
            foreach (IdentityError error in result.Errors)
                ModelState.AddModelError("", error.Description);
        }

        public async Task<IActionResult> Delete(string id) {
            AppUser user = await userManager.FindByIdAsync(id);
            if (user != null) {
                IdentityResult result = await userManager.DeleteAsync(user);
                if (result.Succeeded)
                    return RedirectToAction("Index");
                else
                    Errors(result);
            }
            else
                ModelState.AddModelError("", "User Not Found");
            return View("Index", userManager.Users);
        }

    }

}
