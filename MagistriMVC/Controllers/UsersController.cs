using MagistriMVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MagistriMVC.Controllers {
    public class UsersController : Controller {
        private UserManager<AppUser> userManager;

        public UsersController(UserManager<AppUser> userManager) {
            this.userManager = userManager;
        }

        public IActionResult Index() {
            return View();
        }
    }
}
