using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;

namespace ITI.E_Commerce.Presentation
{
    public class FilterImages : ValidationAttribute
    {
        string[] extensions;
        public FilterImages(string[] _extensions)
        {
            extensions = _extensions;
        }
        protected override ValidationResult IsValid(

        object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }
            else
            {
                var file = value as IFormFile;
                var extension = Path.GetExtension(file.FileName);
                if (file != null)
                {
                    if (!extensions.Contains(extension.ToLower()))
                    {
                        return new ValidationResult("extensions not valied" + (file.FileName));
                    }
                }
                return ValidationResult.Success;
            }
        }

    }
}
