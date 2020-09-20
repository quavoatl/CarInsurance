using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace CarInsurance.ConstantsAndHelpers.CustomModelValidations
{
    public class CarEngineCCValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            int engineCC = (int)value;

            if (engineCC > 999 && engineCC < 5000) return ValidationResult.Success;
            else return new ValidationResult($"{(int)value} is not a valid EngineCC !! ");
        }

    }
}
