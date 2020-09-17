using System;
using System.Collections.Generic;
using System.Text;

namespace CarInsurance.DataAccessV3.DbModels
{
    public class Limit
    {
        public Limit()
        {
            LimitRule = new List<LimitRule>();
        }

        public Guid LimitCoverId { get; set; }
        public int LimitId { get; set; }
        public string CoverType { get; set; }
        public string Name { get; set; }
        public string LimitValues { get; set; }
        public Guid LimitRuleCoverId { get; set; } //FK

        public ICollection<LimitRule> LimitRule { get; set; } //cand useru alege limita din dropdown incarca regulile si verifica daca se aplica multiply
        public Cover Cover { get; set; }
    }
}
