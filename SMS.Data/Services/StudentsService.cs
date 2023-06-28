
using Microsoft.EntityFrameworkCore;
using SMS.Models.Models;

namespace SMS.Data.Services {
    
    public class StudentsService {      //Service provadi veschny samotne funkce pro praci s daty
        public ApplicationDbContext DbContext;          
        public StudentsService(ApplicationDbContext DbContext) {     //posilam tady databazi jako parametr
            this.DbContext = DbContext;
        }

        public async Task<IEnumerable<Student>> getAllAsync() {     //async vzdy vraci Task
            return await DbContext.Students.ToListAsync();          //v tomto pripade v tasku vracim generickou kolekci(ma mene metod nez klasicka kolekce) (IEnumerable) typu Student
        }

        public async Task CreateAsync(Student newStudent) {         //vkladani studenta do databaze a ulozeni
            await DbContext.Students.AddAsync(newStudent);
            await DbContext.SaveChangesAsync();

        }

        public async Task<Student>  GetByIdAsync(int id) {
            return await DbContext.Students.FirstOrDefaultAsync(s => s.Id == id);

        }

        public async Task<Student> UpdateAsync(int id, Student updatedStudent) {
            DbContext.Students.Update(updatedStudent);
            await DbContext.SaveChangesAsync();
            return updatedStudent;
        }

        public async Task DeleteAsync(int id) {
            var studentToDelete = await DbContext.Students.FirstOrDefaultAsync(s => s.Id == id);
            DbContext.Students.Remove(studentToDelete);
            await DbContext.SaveChangesAsync();
        }
    }
}
