using System.ComponentModel.DataAnnotations;

namespace ITI.E_Commerce.Presentation.Models
{
    public class ChangePasswordModel
    {
           [Required, MinLength(4), MaxLength(16)]
            //Display(Name = "Current Password")]
           [ DataType(DataType.Password)]
        public string CurrentPassword { get; set; }
           [Required, MinLength(4), MaxLength(16)]
            //Display(Name = "New Password")]
           [ DataType(DataType.Password)]
        public string NewPassword { get; set; }
            [Compare("NewPassword")]
            //[ Display(Name = "Confirm New Password")]
            [ DataType(DataType.Password)]
        public string ConfirmNewPassword { get; set; }

    }
}
