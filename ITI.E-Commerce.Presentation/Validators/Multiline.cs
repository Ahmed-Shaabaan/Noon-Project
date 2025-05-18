using System.ComponentModel.DataAnnotations;
namespace ITI.E_Commerce.Presentation.Validators
{
    public class Multiline : ValidationAttribute
    {
        public int CountOfLines { get; set; } = 2;
        public string CustomErr { get; set; }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(value?.ToString()))
                return new ValidationResult("Must Insret Value");
            string[] lines =
            value.ToString().Split(Environment.NewLine);
            if (lines.Length < CountOfLines)
                return new ValidationResult(CustomErr ?? $" Must Be More Than Or Equals {CountOfLines} ");
            return ValidationResult.Success;
        }
    }
}
