
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystem.Models;
using SchoolManagementSystem.Services;

namespace SchoolManagementSystem.Controllers {
    [Authorize(Roles ="Admin")]
    public class SubjectsController : Controller {
        public SubjectsService service { get; set; }
        public SubjectsController(SubjectsService service) {
            this.service = service;

        }
        [HttpGet]
        public async Task<IActionResult> Index() {         //odsud volam asynchronni metodu ze SubjectsService
            var allSubjects = await service.GetSubjectsAsync();
            return View(allSubjects);
        }

        [HttpGet]
        public IActionResult Create() {             //zobrazeni View s nazvem "Create" (UI)
            return View();
        }

        [HttpPost]      //vkladam data
        public async Task<IActionResult> Create(Subject newSubject) {       //predmet napsany ve formulari
            if (ModelState.IsValid) {
                await service.CreateAsync(newSubject);      //zavolam metodu pro vytvoreni predmetu
                return RedirectToAction("Index");           //zavolani akce/metody Index
            }
            else {
                return View();          //validace s prazdnym view
            }

        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id) {
            var subjectToEdit = await service.GetByIdAsync(id);
            if (subjectToEdit == null) {
                return View("NotFound");
            }
            return View(subjectToEdit);
        }

        [HttpPost]      //vkladam data
        public async Task<IActionResult> Edit(int id, [Bind("Id, Name")] Subject subject) {
            await service.UpdateAsync(id, subject);
            return RedirectToAction("Index");
        }
        //[HttpDelete]		slozitejsi problem, proc to nejde, vratit se...
        public async Task<IActionResult> Delete(int id) {
            var subjectToDelete = await service.GetByIdAsync(id);
            if (subjectToDelete == null) {
                return View("NotFound");
            }
            await service.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}
