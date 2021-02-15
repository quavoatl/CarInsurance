using System;
using System.Collections.Generic;

#nullable disable

namespace CarInsurance.DataAccessV2.DbModels
{
    public partial class Cover
    {
        public Cover()
        {
            Limits = new HashSet<Limit>();
            Questions = new HashSet<Question>();
        }

        public int CoverId { get; set; }
        public Guid CoverBrokerRefId { get; set; }
        public string Type { get; set; }
        public DateTime AdditionDate { get; set; }
        public Guid QuestionCoverId { get; set; }
        public Guid LimitCoverId { get; set; }

        public virtual BrokerPolicyTemplate CoverBrokerRef { get; set; }
        public virtual ICollection<Limit> Limits { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
    }
}
