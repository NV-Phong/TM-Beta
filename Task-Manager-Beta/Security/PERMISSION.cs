using Microsoft.AspNetCore.Identity;

namespace Task_Manager_Beta.Security
{
    public class PERMISSION: IdentityRole
    {
        public const string Role_Company = "Company";
        public const string Role_Admin = "Admin";
        public const string Role_NormalUser = "NormalUser";
    }
}
