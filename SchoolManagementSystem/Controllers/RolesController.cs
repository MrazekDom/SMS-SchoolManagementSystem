using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SMS.Models.Models;
using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Controllers {
    [Authorize(Roles ="Admin")]
    public class RolesController : Controller {
        private RoleManager<IdentityRole> roleManager;      //in-built servicka
        private UserManager<AppUser> userManager;       //budu pracovat i s uzivateli, prirazovani roli

        public RolesController(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager) {
            this.roleManager = roleManager;
            this.userManager = userManager;
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

        [HttpGet]
        public async Task<IActionResult> Edit(string id) {
            IdentityRole role = await roleManager.FindByIdAsync(id);
            List<AppUser> members = new List<AppUser>();
            List<AppUser> nonMembers = new List<AppUser>();
            foreach (AppUser user in userManager.Users) {   //vyuziti servicky userManager
                var list = await userManager.IsInRoleAsync(user, role.Name) ? members : nonMembers; //ternanrni vyraz, bud se prida do kolekce members nebo nonMembers
                list.Add(user);
            }
            return View(new RoleEdit {
                Role = role,
                Members = members,
                NonMembers = nonMembers
            });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(RoleModification model) {
            IdentityResult result;
            if (ModelState.IsValid) {
                foreach (string userId in model.AddIds ?? new string[] { }) {       //null coalescing operator, z tutorialu , kdyz je nalevo null, provede prikaz na prave strane
                    AppUser user = await userManager.FindByIdAsync(userId);     //nove pole se vytvori proto, abychom se vyhli nullReferenceExceptions pri uziti forEach
                    if (user != null) {
                        result = await userManager.AddToRoleAsync(user, model.RoleName);
                        if (!result.Succeeded)
                            Errors(result);
                    }
                }
                foreach (string userId in model.DeleteIds ?? new string[] { }) {
                    AppUser user = await userManager.FindByIdAsync(userId);
                    if (user != null) {
                        result = await userManager.RemoveFromRoleAsync(user,
                        model.RoleName);
                        if (!result.Succeeded)
                            Errors(result);
                    }
                }
            }
            if (ModelState.IsValid)
                return RedirectToAction("Index");
            else
                return await Edit(model.RoleId);
        }

        private void Errors(IdentityResult result) {
            foreach (IdentityError error in result.Errors)
                ModelState.AddModelError("", error.Description);
        }
    }
}