using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CarInsurance.ConstantsAndHelpers.CustomModelValidations
{
    public class CarBrandValidation : ValidationAttribute
    {
        private List<string> allowedBrands = null;

        public CarBrandValidation()
        {
            allowedBrands = new List<string>()
        {
            "skoda","toyota","renault","dacia","opel","mercedes-benz","bmw","volkswagen"
        };
        }

        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            if (allowedBrands.Contains((string)value)) return ValidationResult.Success;
            else return new ValidationResult($"{(string)value} invalid brand name !! ");
        }


    }
}
