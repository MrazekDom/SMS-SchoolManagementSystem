using SchoolManagementSystem.Models;

namespace SchoolManagementSystem.ViewModels{
    public class StudentsDropDownViewModel {
        public StudentsDropDownViewModel() {
            Students = new List<Student>();
        }

        public List<Student> Students { get; set; } 
    }
}
