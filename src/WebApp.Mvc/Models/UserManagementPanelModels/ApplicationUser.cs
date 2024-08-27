using Microsoft.AspNetCore.Identity;

namespace WebApp.Mvc.Models.UserManagementPanelModels
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int UsernameChangeLimit { get; set; } = 10;
        public byte[] ProfilePicture { get; set; }
    }


    public enum Roles
    {
        SuperAdmin,
        Admin,
        Moderator,
        Basic
    }

    public enum Claims
    {
        SuperAdmin,
        Admin,
        Moderator,
        Basic
    }
}


