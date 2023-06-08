using SchoolManagementSystem.Models;

namespace SchoolManagementSystem.ViewModels {
    public class GradesDropDownViewModel {      //view model, slouzi k tomu, abychom do View dostaly vsechny udaje
        public List<Student> Students { get; set; } //co potrebujeme, protoze View nebere vice jak 1 model
        public List<Subject> Subjects { get; set; }

        public GradesDropDownViewModel() {
            Students = new List<Student>();
            Subjects = new List<Subject>();
        }
    }
}
