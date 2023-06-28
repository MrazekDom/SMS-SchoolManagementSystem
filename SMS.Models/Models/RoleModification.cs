namespace SMS.Models.Models{
    public class RoleModification {    //pro samotne zmeny v rolich([HttpPost] Edit)
        public string RoleName { get; set; }
        public string RoleId { get; set; }
        public string[]? AddIds { get; set; }        //pole Idcek, kterym danou roli davam
        public string[]? DeleteIds { get; set; }      //pole Idcek, kterzm danou roli odebiram
    }
}
