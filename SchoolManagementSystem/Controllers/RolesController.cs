using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace SchoolManagementSystem.Controllers {
    public class RolesController : Controller {
        private RoleManager<IdentityRole> roleManager;

        public RolesController(RoleManager<IdentityRole> roleManager) {
            this.roleManager = roleManager;
        }

        public IActionResult Index() { 
            return View(roleManager.Roles);
        }

        private void Errors(IdentityResult result) {
            foreach (IdentityError error in result.Errors)
                ModelState.AddModelError("", error.Description);
        }
    }
}