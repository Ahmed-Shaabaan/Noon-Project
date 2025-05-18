using System;
namespace ITI.E_Commerce.Presentation.Models.CategoryModel
{
	public class CategoryModel
	{
		public int Id { get; set; }
        public string Name { get; set; }
        //[Required(ErrorMessage = "Must Enter images")]
        [FilterImages(new string[] { ".png", ".jpg", ".jpeg" })]
        public IFormFile Image { get; set; }

    }
}

