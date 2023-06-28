using SMS.Models.Models;

namespace SMS.Models.ViewModels {
    public class StudentsDropDownViewModel {
        public StudentsDropDownViewModel() {
            Students = new List<Student>();
        }

        public List<Student> Students { get; set; } 
    }
}
