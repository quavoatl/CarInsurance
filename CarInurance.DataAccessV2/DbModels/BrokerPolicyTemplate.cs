using System;
using System.Collections.Generic;

#nullable disable

namespace CarInsurance.DataAccessV2.DbModels
{
    public partial class BrokerPolicyTemplate
    {
        public BrokerPolicyTemplate()
        {
            Cars = new HashSet<Car>();
            Covers = new HashSet<Cover>();
        }

        public Guid BrokerReferenceId { get; set; }
        public DateTime CreationDate { get; set; }
        public Guid CarBrokerRefId { get; set; }
        public Guid CoverBrokerRefId { get; set; }
        public bool TemplateReady { get; set; }

        public virtual ICollection<Car> Cars { get; set; }
        public virtual ICollection<Cover> Covers { get; set; }
    }
}
