using System.ComponentModel.DataAnnotations;

namespace AutoSzerelo.Contracts.Validations;

public class GyartasiEvRangeAttribute : ValidationAttribute
{
    private const int MinEv = 1900;

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is int ev)
        {
            int maxEv = DateTime.Now.Year;
            if (ev < MinEv || ev > maxEv)
            {
                return new ValidationResult(
                    $"A gyártási évnek {MinEv} és {maxEv} között kell lennie.",
                    [validationContext.MemberName]);
            }
        }

        return ValidationResult.Success;
    }
}