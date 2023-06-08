
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolManagementSystem.Services;
using SchoolManagementSystem.ViewModels;

namespace SchoolManagementSystem.Controllers {
    [Authorize]
    public class GradesController : Controller {
        public GradesService service { get; set; }

        public GradesController(GradesService service) {
            this.service = service;
        }

        public async Task<IActionResult> Index() {
            var allGrades = await service.GetAllAsync();
            return View(allGrades);     //odkazuju na view a predavam mu data (allGrades)
        }

        public async Task<IActionResult> Create() {             //zobrazeni View s nazvem "Create" (UI)
            var gradesDropDownData = await service.GetNewGradesDropDownsValues();   //volani metody pro naplneni ViewModelu
            ViewBag.Students = new SelectList(gradesDropDownData.Students, "Id", "LastName"); //The SelectList class is used to create a list of SelectListItem objects that can be used to populate a dropdown list in a Razor view.
            ViewBag.Subjects = new SelectList(gradesDropDownData.Subjects, "Id", "Name");
            return View();
        }

        [HttpPost]      //vkladam data
        public async Task<IActionResult> Create(GradesVM newGrade) {       //object typu GradesVM napsany ve formulari
            if (!ModelState.IsValid) { //jestlize uzivatel vyplnil tabulku spatne nebo nevyplnil, tak se mu zobrazi znova 
                var gradesDropdownsData = await service.GetNewGradesDropDownsValues();
                ViewBag.Students = new SelectList(gradesDropdownsData.Students, "Id", "LastName");
                ViewBag.Subjects = new SelectList(gradesDropdownsData.Subjects, "Id", "Name");
                return View(newGrade);      //zobrazi view (UI) kteremu v parametru predava objekt s nazvem newGrade
            }
            await service.CreateAsync(newGrade); //zapise do databaze pres servisku
            return RedirectToAction("Index"); //zobrazi view Index 
        }

        [HttpGet]

        public async Task<IActionResult> Edit(int id) {
            var gradeToEdit = await service.GetByIdAsync(id);
            if (gradeToEdit == null) {
                return View("NotFound");
            }
            var response = new GradesVM() {
                Id = gradeToEdit.Id,
                Date = gradeToEdit.Date,
                Mark = gradeToEdit.Mark,
                StudentId = gradeToEdit.Student.Id,
                SubjectId = gradeToEdit.Subject.Id,
                What = gradeToEdit.What
            };

            var gradesDropdownsData = await service.GetNewGradesDropDownsValues();
            ViewBag.Students = new SelectList(gradesDropdownsData.Students, "Id", "LastName");
            ViewBag.Subjects = new SelectList(gradesDropdownsData.Subjects, "Id", "Name");
            return View(response);
        }

        public async Task<IActionResult> Delete(int id) {
            var gradeToDelete = await service.GetByIdAsync(id);
            await service.DeleteAsync(id);
            return RedirectToAction("Index");
        }

        [HttpPost]      //vkladam data
        public async Task<IActionResult> Edit(int id, [Bind("Id, StudentId, SubjectId, What, Mark, Date")] GradesVM gradeToUpdate) {
            await service.UpdateAsync(id, gradeToUpdate);
            return RedirectToAction("Index");
        }


    }

}
