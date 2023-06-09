using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Controllers {
    [Authorize]
    public class RolesController : Controller {
        private RoleManager<IdentityRole> roleManager;      //in-built servicka

        public RolesController(RoleManager<IdentityRole> roleManager) {
            this.roleManager = roleManager;
        }

        public IActionResult Index() { 
            return View(roleManager.Roles);
        }
        [HttpGet] //pouze vraci View
        public IActionResult Create() {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Required] string name) {
            if (ModelState.IsValid) {
                IdentityResult result = await roleManager.CreateAsync(new IdentityRole(name));
                if (result.Succeeded)
                    return RedirectToAction("Index");
                else
                    Errors(result);
            }
            return View(name);      //jestlize "name" neni Valid, tak se pouze zobrazi znova View, kde je name vypsany
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id) {
            IdentityRole role = await roleManager.FindByIdAsync(id);
            if (role != null) {
                IdentityResult result = await roleManager.DeleteAsync(role);
                if (result.Succeeded)
                    return RedirectToAction("Index");
                else
                    Errors(result);
            }
            else
                ModelState.AddModelError("", "No role found");
            return View("Index", roleManager.Roles);
        }

        private void Errors(IdentityResult result) {
            foreach (IdentityError error in result.Errors)
                ModelState.AddModelError("", error.Description);
        }
    }
}