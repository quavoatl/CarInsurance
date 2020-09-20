using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CarInsurance.ConstantsAndHelpers.CustomModelValidations
{
    public class CarEuroStandardValidation : ValidationAttribute
    {
        private List<string> allowedStandards = null;

        public CarEuroStandardValidation()
        {
            allowedStandards = new List<string>() {"Euro1","Euro2","Euro3","Euro4","Euro5","Euro6" };
        }

        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            if (allowedStandards.Contains((string)value)) return ValidationResult.Success;
            else return new ValidationResult($"{(string)value} not supported standard !! ");
        }


    }
}
