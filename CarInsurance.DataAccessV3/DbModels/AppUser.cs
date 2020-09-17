using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CarInsurance.DataAccessV3.DbModels
{
    public class AppUser : IdentityUser
    {
        public string Address { get; set; }
        public bool IsBroker { get; set; }
    }
}
