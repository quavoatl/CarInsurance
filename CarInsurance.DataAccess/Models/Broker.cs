using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using CarInsurance.DataAccess.ModelsPOCOs;

namespace CarInsurance.DataAccess.Models
{
    public class Broker
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime CreationDate { get; set; }

        [Key]
        public BrokerDetails BrokerDetails { get; set; }

        [NotMapped]
        public PolicyTemplate PolicyTemplate = new PolicyTemplate();
    }
}
