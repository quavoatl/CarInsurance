using CarInsurance.DataAccess.InfrastructureObjects.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CarInsurance.DataAccess.Models
{
    public class TheftLimit
    {
        [Key]
        public Guid ThefLimittBrokerId { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(20)")]
        public  string Name { get; set; }
        public  string LimitValues { get; set; }
    }
}
