using System.ComponentModel.DataAnnotations;

namespace ITI.E_Commerce.Presentation.Models
{
    public class RoleCreateModel
    {
        [Required]
        [MaxLength(100)]
        [MinLength(3)]
        public string Name { get; set; }
    }
}
