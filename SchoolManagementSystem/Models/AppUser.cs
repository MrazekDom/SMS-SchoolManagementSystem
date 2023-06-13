using Microsoft.AspNetCore.Identity;

namespace SchoolManagementSystem.Models {
    public class AppUser : IdentityUser {     //dedi vlastnisto z IdentityUser (UserName, Email, PhoneNumber...)
        //public List<Student>? AssignedStudents { get; set; }        //nove

        //public AppUser() {
        //    AssignedStudents = new List<Student>();
        //}
        public ICollection<AppUserStudent> AppUserStudents { get; set; }
    }
}
