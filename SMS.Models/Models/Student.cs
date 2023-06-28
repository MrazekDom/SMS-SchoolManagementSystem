using System.ComponentModel.DataAnnotations;

namespace SMS.Models.Models {
    public class Student {
        public int Id { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]        //anotace
        public string LastName { get; set; }
        [Display(Name = "Date of birth")]    //anotace pro zobrazeni v UI
        public DateTime DateOfBirth { get; set; }

        public ICollection<AppUserStudent> AppUserStudents { get; set; }
		//public List<AppUser>? AssignedUsers { get; set; }  //nove

		public ICollection<Grade> Grades { get; set; } // Navigation property
	}
}
