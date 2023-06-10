using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.ViewModels {
    public class UserVM {

        [Required]
        [Display(Name = "User name")]
        public string Name { get; set; }
        [Required]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

        public int AssignedStudentId { get; set; } //nove
    }
}
