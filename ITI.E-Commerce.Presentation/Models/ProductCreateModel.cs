using ITI.E_Commerce.Presentation.Validators;
using System.ComponentModel.DataAnnotations;
namespace ITI.E_Commerce.Presentation.Models
{
    public class ProductCreateModel
    {
       [Display(Name = "Name")]
        [Required]
        //[MaxLength(500, ErrorMessage = "Product Name Musn't Exceeed 500")]
        //[MinLength(7, ErrorMessage = "Must Be More Than Or Equals 4 Chars.")]
        public string Name { get; set; }
        [Required, Display(Name = "Quantity")]
        public int Quantity { get; set; }
        [Required, Display(Name = "UnitPrice")]
        public int UnitPrice { get; set; }
        [Display(Name = "Description"), Required]
        //[DataType(DataType.MultilineText)]
        //[Multiline(CountOfLines = 3, CustomErr = "Must Be Three Lines")]
        public string Description { get; set; }
       [Display(Name = "Discount")]
        public double Discount { get; set; }

        //[Required, Display(Name = "Color")]
        public string Color { get; set; }

        [Required, Display(Name = "Size")]
        public string Size { get; set; }
      // [ Display(Name = "NewPrece")]
        public int NewPrece { get; set; }
        
        [Required, Display(Name = "CategoryID")]
        public int CategoryID { get; set; }

        [Required, Display(Name= "BrandID")]
        public int BrandID { get; set; }

        //[Required, Display(Name = "ProductInStock")]
        //public int ProductInStock { set; get; }
       [Required, Display(Name = "Images")]
        public List<IFormFile> Images { get; set; }
    }
}