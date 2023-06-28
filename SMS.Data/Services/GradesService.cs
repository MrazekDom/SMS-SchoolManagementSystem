
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SMS.Models.Models;
using SMS.Models.ViewModels;

namespace SMS.Data.Services {
    public class GradesService {
        public ApplicationDbContext DbContext { get; set; }
        private UserManager<AppUser> _userManager;
		public GradesService(ApplicationDbContext context, UserManager<AppUser> userManager) {
			DbContext = context;
			_userManager = userManager;
		}
		public async Task<GradesDropDownViewModel> GetNewGradesDropDownsValues() {      //metoda pro naplneni ViewModelu
            var gradesDropdownsData = new GradesDropDownViewModel() {
                Students = await DbContext.Students.OrderBy(n => n.LastName).ToListAsync(),
                Subjects = await DbContext.Subjects.OrderBy(x => x.Name).ToListAsync(),
            };
            return gradesDropdownsData;

        }
        public async Task CreateAsync(GradesVM newGrade) {         //vkladani znamky do databaze a ulozeni, pouzivam viewModel GradesVM
            var gradeToInsert = new Grade() {       //nova znamka na vlozeni, novy object typu Grade
                Student = await DbContext.Students.FirstOrDefaultAsync(s => s.Id == newGrade.StudentId),        //propojuji jmeno sstudenta a predmet s daty co uz v databazi jsou, podle ID
                Subject = await DbContext.Subjects.FirstOrDefaultAsync(sub => sub.Id == newGrade.SubjectId),
                Date = DateTime.Today,
                What = newGrade.What,           //do noveho objectu typu Grade vkladam data ktere byly predany v objektu typu GradesVM
                Mark = newGrade.Mark
            };
            if (gradeToInsert.Student != null && gradeToInsert.Subject != null) {   //kdyz neni policko na jmeno a predemt prazdne
                await DbContext.Grades.AddAsync(gradeToInsert);     //samotne vlozeni do databaze 
                await DbContext.SaveChangesAsync(); //ulozeni zmen
            }
        }

        public async Task<IEnumerable<Grade>> GetAllAsync() {
            return await DbContext.Grades.Include(n => n.Student).Include(c => c.Subject).ToListAsync();        //v objektu Grade jsou vlastnosti typu Subject a Student, ktere uz maji vlastni tabulky v databazi
        }

        public async Task<Grade> GetByIdAsync(int id) {
            return await DbContext.Grades.Include(n => n.Student).Include(c => c.Subject).FirstOrDefaultAsync(s => s.Id == id);

        }

        public async Task<IEnumerable<Grade>> GetSomeAsync(string id) {
			AppUser user = await _userManager.FindByIdAsync(id);    //najdu si usera podle Idcka
			List<int> StudentsIds = await DbContext.AppUserStudents        //udelam si list Idcek jemu prirazenych studentu
			.Where(a => a.AppUserId == user.Id)
			.Select(a => a.Student.Id)
			.ToListAsync();
			List<Grade> grades = await DbContext.Grades     //vytvorim kolekci znamek, kde Includnu i jejich Properties (Student + Subject)
	        .Include(g => g.Student)
	        .Include(g => g.Subject)
	        .Where(g => StudentsIds.Contains(g.Student.Id))     //vlozim ty znamky, kde se shoduji Id v z listu StudentsIds s Id studentu v tabulce Grades
	        .ToListAsync();
			return grades;
            
		}

        public async Task UpdateAsync(int id, GradesVM gradeToUpdate) {
            var dbGrade = await DbContext.Grades.FirstOrDefaultAsync(n => n.Id == gradeToUpdate.Id);
            if (dbGrade != null) {
                dbGrade.Student = DbContext.Students.FirstOrDefault(n => n.Id == gradeToUpdate.StudentId);  //jmeno studenta v nove znamce se priradi podle ID, ktere se predalo v gradeToUpdate a najde se v databazi
                dbGrade.Subject = DbContext.Subjects.FirstOrDefault(s => s.Id == gradeToUpdate.SubjectId);
                dbGrade.What = gradeToUpdate.What;
                dbGrade.Mark = gradeToUpdate.Mark;
                dbGrade.Date = DateTime.Now;        //datum zmeny, TED
            }
            DbContext.Update(dbGrade);
            await DbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id) {
            var gradeToDelete = DbContext.Grades.FirstOrDefault(s => s.Id == id);
            DbContext.Grades.Remove(gradeToDelete);
            await DbContext.SaveChangesAsync();

        }
    }
}
