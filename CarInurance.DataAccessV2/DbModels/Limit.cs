using System;
using System.Collections.Generic;

#nullable disable

namespace CarInsurance.DataAccessV2.DbModels
{
    public partial class Limit
    {
        public Limit()
        {
            LimitRules = new HashSet<LimitRule>();
        }

        public int LimitId { get; set; }
        public Guid LimitCoverId { get; set; }
        public string Name { get; set; }
        public Guid LimitRuleCoverId { get; set; }

        public virtual Cover LimitCover { get; set; }
        public virtual ICollection<LimitRule> LimitRules { get; set; }
    }
}
