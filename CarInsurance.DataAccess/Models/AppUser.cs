using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CarInsurance.DataAccess.Models
{
    public class AppUser : IdentityUser
    {
        [Column(TypeName = "nvarchar(20)")]
        public string Address { get; set; }
        [Column(TypeName = "bit")]
        public bool IsBroker { get; set; }
    }
}
