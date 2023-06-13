using SchoolManagementSystem.Models;
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

        [Display(Name ="Assign a student to a user:")]
        public int[]? AssignedStudentId { get; set; } //pole pro IDcka prirazenych studentu k danemu uctu
        //musim to tu nechat, protoze prirazuju uzivateli Studenty na zakldae jejich unikatniho Id, ne jmena (vice studentu se muze jmenovat stejne)
        //vyuzito v "Create" view

        [Display(Name ="List of students already assigned to this user:")]
        public List<Student>? AssignedStudentsList { get; set; }    //vyuzito v "ConnectStudents" view

        ///
        public string? UserIdToView { get; set; }

    }
}
