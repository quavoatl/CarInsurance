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
            allowedStandards = new List<string>() {"euro1","euro2","euro3","euro4","euro5","euro6" };
        }

        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            if (allowedStandards.Contains(value.ToString().ToLower())) return ValidationResult.Success;
            else return new ValidationResult($"{(string)value} not supported standard !! ");
        }


    }
}
