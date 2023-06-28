using System.ComponentModel.DataAnnotations;

namespace SMS.Models.ViewModels {
    public class GradesVM {
        public int Id { get; set; }
        [Display(Name = "Student Name")]
        public int StudentId { get; set; }
        [Display(Name = "Subject")]
        public int SubjectId { get; set; }
        public string What { get; set; }
        [Range(1, 5, ErrorMessage = "Mark must be a number between 1 and 5.")]
        public int Mark { get; set; }
        public DateTime Date { get; set; }
    }
}
