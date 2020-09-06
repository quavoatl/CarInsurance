using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CarInsurance.DataAccess.InfrastructureObjects.Interfaces
{
    public class Limit
    {
        [Required]
        [MinLength(5)]
        [Display(Name = "Please enter a name for your limit, EX: SuperLimit")]
        public string Name { get; set; }
        [Required]

        public int LimitValue { get; set; }
        public ICollection<int> LimitValues { get; set; }
    }
}
