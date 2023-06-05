using MagistriMVC.Models;
using MagistriMVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace MagistriMVC.Controllers {
	public class SubjectsController : Controller {
		public SubjectsService service { get; set; }
		public SubjectsController(SubjectsService service) { 
			this.service = service;
			
		}
		[HttpGet]
		public async Task<IActionResult> Index() {         //odsud volam asynchronni metodu ze SubjectsService
			var allStudents = await service.GetSubjectsAsync();
			return View(allStudents);
		}

		[HttpGet]
		public IActionResult Create() {             //zobrazeni View s nazvem "Create" (UI)
			return View();
		}

		[HttpPost]      //vkladam data
		public async Task<IActionResult> Create(Subject newStudent) {       //predmet napsany ve formulari
			await service.CreateAsync(newStudent);      //zavolam metodu pro vytvoreni predmetu
			return RedirectToAction("Index");           //zavolani akce/metody Index
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
