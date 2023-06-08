using MagistriMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace MagistriMVC.Controllers {
    public class HomeController : Controller {
        private UserManager<AppUser> userManager;

        public HomeController(UserManager<AppUser> userManager) {
            this.userManager = userManager;
        }

        [Authorize]     //cokoliv, co je pod timto, tak se neprihlaseny k tomu nedostane
        public async Task<IActionResult> Index() {
            AppUser loggedInUser = await userManager.GetUserAsync(HttpContext.User);
            string message = "Welcome, " + loggedInUser.UserName;
            return View("Index", message);
        }

        //public IActionResult Privacy() {
        //    return View();
        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}