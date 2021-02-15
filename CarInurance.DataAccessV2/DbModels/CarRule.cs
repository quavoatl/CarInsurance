using System;
using System.Collections.Generic;

#nullable disable

namespace CarInsurance.DataAccessV2.DbModels
{
    public partial class CarRule
    {
        public int CarRuleId { get; set; }
        public Guid CarRuleCoverId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float Multiplier { get; set; }

        public virtual Car CarRuleCover { get; set; }
    }
}
