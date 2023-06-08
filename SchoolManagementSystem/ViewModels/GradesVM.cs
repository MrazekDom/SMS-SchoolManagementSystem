using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.ViewModels {
    public class GradesVM {
        public int Id { get; set; }
        [Display(Name = "Student Name")]
        public int StudentId { get; set; }
        [Display(Name = "Subject")]
        public int SubjectId { get; set; }
        public string What { get; set; }
        public int Mark { get; set; }
        public DateTime Date { get; set; }
    }
}
