using System.ComponentModel.DataAnnotations;

namespace ITI.E_Commerce.Presentation.Models
{
    // Types and Validate Data  request From Form Login  
    public class LoginModel
    {
        [Required]
        //[Display(Name = "UserName")]
        public string UserName { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(16)]
        //[Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }
}
