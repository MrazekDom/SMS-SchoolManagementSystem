using Microsoft.AspNetCore.Identity;

namespace SchoolManagementSystem.Models {
    public class AppUser : IdentityUser {     //dedi vlastnisto z IdentityUser (UserName, Email, PhoneNumber...)
        public List<Student>? AssignedStudents { get; set; }
    }
}
