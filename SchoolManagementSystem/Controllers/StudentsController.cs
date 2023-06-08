
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystem.Models;
using SchoolManagementSystem.Services;

namespace SchoolManagementSystem.Controllers {
    [Authorize]
    public class StudentsController : Controller {
        public StudentsService service;
        public StudentsController(StudentsService service)      //davam controlleru "service" jako parametr
        {                                                       //z kontroleru se volaji metody ze Service, ktere az zpracovavaji data
            this.service = service;
        }
        [HttpGet]
        public async Task<IActionResult> Index() {         //odsud volam asynchronni metodu ze StudentsService
            var allStudents = await service.getAllAsync();
            return View(allStudents);
        }

        public IActionResult Create() {             //zobrazeni View s nazvem "Create" (UI)
            return View();
        }
        [HttpPost]      //vkladam data
        public async Task<IActionResult> Create(Student newStudent) {       //student napsany ve formulari
            await service.CreateAsync(newStudent);      //zavolam metodu pro vytvoreni studenta
            return RedirectToAction("Index");           //zavolani akce/metody Index
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id) {         //metoda pro zobrazeni stranky edit pro danou instanci
            var studentToEdit = await service.GetByIdAsync(id);
            if (studentToEdit == null) {
                return View("NotFound");
            }
            return View(studentToEdit);
        }

        [HttpPost]      //vkladam data
        public async Task<IActionResult> Edit(int id, [Bind("Id, FirstName, LastName, DateOfBirth")] Student student) {
            await service.UpdateAsync(id, student);
            return RedirectToAction("Index");
        }

        //[HttpDelete]         slozitejsi problem, proc nejde, vratit se k tomu pozdeji
        public async Task<IActionResult> Delete(int id) {
            var studenToDelete = await service.GetByIdAsync(id);
            if (studenToDelete == null) {
                return View("NotFound");
            }
            await service.DeleteAsync(id);
            return RedirectToAction("Index");
        }


    }
}
