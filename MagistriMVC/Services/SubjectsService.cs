using MagistriMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace MagistriMVC.Services {
	public class SubjectsService {
		public ApplicationDbContext DbContext { get; set; }

		public SubjectsService(ApplicationDbContext DbContext) {
			this.DbContext = DbContext;
		}

		public async Task<IEnumerable<Subject>> GetSubjectsAsync() {
			return await DbContext.Subjects.ToListAsync();
		}

		public async Task CreateAsync(Subject newSubject) {       
			await DbContext.Subjects.AddAsync(newSubject);
			await DbContext.SaveChangesAsync();

		}

		public async Task<Subject> GetByIdAsync(int id) {
			return await DbContext.Subjects.FirstOrDefaultAsync(s => s.Id == id);

		}

		public async Task<Subject> UpdateAsync(int id, Subject updatedSubject) {
			DbContext.Subjects.Update(updatedSubject);
			await DbContext.SaveChangesAsync();
			return updatedSubject;
		}

		public async Task DeleteAsync(int id) {
			var subjectToDelete = await DbContext.Subjects.FirstOrDefaultAsync(s => s.Id == id);
			DbContext.Subjects.Remove(subjectToDelete);
			await DbContext.SaveChangesAsync();
		}
	}
}
