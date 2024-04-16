using System.ComponentModel.DataAnnotations;
using System.Drawing.Printing;
using Task_Manager_Beta.Data;

namespace Task_Manager_Beta.ViewModels
{
    public class InviteUserViewModel
    {
        [Required]
        [EmailAddress]
        public string UserEmail { get; set; }

    }
}
