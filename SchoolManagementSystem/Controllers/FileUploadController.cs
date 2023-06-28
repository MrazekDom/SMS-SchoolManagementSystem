using Microsoft.AspNetCore.Mvc;
using SMS.Data.Services;
using SMS.Models.Models;
using System.Globalization;
using System.Xml;

namespace SchoolManagementSystem.Controllers
{
    public class FileUploadController : Controller {
		StudentsService studentsService;

		public FileUploadController(StudentsService studentsService) {
			this.studentsService = studentsService;
		}

		[HttpPost]
		public async Task<IActionResult> Upload(IFormFile file) {	
			string filePath = "";
			if (file.Length > 0) {
				filePath = Path.GetFullPath(file.FileName);
				using (var stream = new FileStream(filePath, FileMode.Create)) {	//kopirovani XML souboru
					await file.CopyToAsync(stream);
					stream.Close();
					XmlDocument xmlDoc = new XmlDocument();		//objekt pro praci s XML
					xmlDoc.Load(filePath);						//nacteni zkopirovaneho XMLka
					XmlElement root = xmlDoc.DocumentElement;
					foreach (XmlNode node in root.SelectNodes("/Students/Student")) {
						Student s = new Student {
							FirstName = node.ChildNodes[0].InnerText,
							LastName = node.ChildNodes[1].InnerText,
							DateOfBirth = DateTime.Parse(node.ChildNodes[2].InnerText, CultureInfo.CreateSpecificCulture("cs-CZ"))
						};
						await studentsService.CreateAsync(s);
					}
				}
				return RedirectToAction("Index", "Students");
			}
			else return View("NotFound");

		}
	}
}

