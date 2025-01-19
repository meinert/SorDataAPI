namespace SorDataAPI.Validators
{
    using System.ComponentModel.DataAnnotations;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Reuseable ValValidator for SOR code. The validators checks that the SOR code is a 15-digit integer 
    /// </summary>
    public class SorCodeValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null || !(value is string sorCode) || !Regex.IsMatch(sorCode, @"^\d{15}$"))
            {
                return new ValidationResult("SOR code must be a 15-digit integer.");
            }

            return ValidationResult.Success;
        }
    }
}
