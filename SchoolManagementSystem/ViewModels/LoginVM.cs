using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.ViewModels {
    public class LoginVM {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        public string? returnUrl { get; set; }          //je nullable
        public bool Remember { get; set; }
    }
}
