using Microsoft.AspNetCore.Identity;

namespace SMS.Models.Models {
    public class RoleEdit {     //reprezentuje roli a seznam uzivali, kteri danou roli maji nebo nemaji prirazenou ([HttpGet] Edit)
        public IdentityRole Role { get; set; }
        public IEnumerable<AppUser> Members { get; set; }
        public IEnumerable<AppUser> NonMembers { get; set; }
    }
}
