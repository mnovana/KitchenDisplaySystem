using System.ComponentModel.DataAnnotations;

namespace KitchenDisplaySystem.CustomValidation
{
    public class DateInRangeAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            DateTime minDate = new DateTime(2024,1,1);
            DateTime maxDate = DateTime.Now;
            
            if (value == null)
            {
                return ValidationResult.Success;
            }

            if((DateTime)value < minDate || (DateTime)value > maxDate)
            {
                return new ValidationResult(ErrorMessage = "Date not in range");
            }

            return ValidationResult.Success;
        }
    }
}
