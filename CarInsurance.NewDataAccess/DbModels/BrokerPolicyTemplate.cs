using CarInsurance.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarInsurance.NewDataAccess.DbModels
{
    public class BrokerPolicyTemplate
    {
        public BrokerPolicyTemplate()
        {
            Cover = new List<Cover>();
            Car = new List<Car>();
        }

        public Guid BrokerReferenceId { get; set; }
        public DateTime CreationDate { get; set; }
        public Guid CarBrokerRefId { get; set; } //acts as a foreign key to Car class
        public Guid CoverBrokerRefId { get; set; } //acts as a foreign key to Cover class
        public bool TemplateReady { get; set; }

        public ICollection<Cover> Cover { get; set; }
        public ICollection<Car> Car { get; set; }
        public AppUser AppUser { get; set; }

    }
}
