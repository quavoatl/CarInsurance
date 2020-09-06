using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CarInsurance.DataAccess.InfrastructureObjects.Interfaces
{
    public class Question
    {
        [Required]
        public virtual string Name { get; set; }
        [Required]
        public virtual string Text { get; set; }
    }
}
