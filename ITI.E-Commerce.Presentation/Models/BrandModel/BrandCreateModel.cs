using ITI.E_Commerce.Presentation;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ITI.E_Commerce.Presentation.Models
{
    public class BrandCreateModel
    {
        [Display(Name ="Name"), Required(ErrorMessage = "Please Enter The Title")]
        [MaxLength(500, ErrorMessage = "Must be less than 500 Chars")]
        public string Name { get; set; }

        //[Required(ErrorMessage = "Must Enter images")]
        [Display(Name ="Image")]
        [FilterImages(new string[] { ".png", ".jpg" ,".jpeg"})]
        public IFormFile Image { get; set; }
    }
}
