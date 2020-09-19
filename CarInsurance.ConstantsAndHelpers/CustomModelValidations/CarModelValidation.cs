using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CarInsurance.ConstantsAndHelpers.CustomModelValidations
{
    public class CarModelValidation : ValidationAttribute
    {
        public Dictionary<string, List<string>> AllowedModelsOnBrand { get; set; }

        public CarModelValidation()
        {
            AllowedModelsOnBrand = new Dictionary<string, List<string>>();
            AllowedModelsOnBrand.Add("toyota", new List<string>() { "prius", "avensis" });
            AllowedModelsOnBrand.Add("dacia", new List<string>() { "sandero", "logan" });
            AllowedModelsOnBrand.Add("renault", new List<string>() { "clio", "megane" });
            AllowedModelsOnBrand.Add("opel", new List<string>() { "corsa", "astra" });
            AllowedModelsOnBrand.Add("bmw", new List<string>() { "525d", "520d", "530d", "330d", "325d", "320d" });
            AllowedModelsOnBrand.Add("mercedes-benz", new List<string>() { "e220", "c220", "e200", "c200" });
            AllowedModelsOnBrand.Add("volkswagen", new List<string>() { "golf", "passat" });
            AllowedModelsOnBrand.Add("skoda", new List<string>() { "fabia", "rapid", "octavia" });
        }

    }
}
