using System;
using System.Collections.Generic;
using System.Text;

namespace CarInsurance.DataAccess.ModelsPOCOs
{
    public class Limit
    {
        public Limit(string name, List<int> limitValues)
        {
            this.Name = name;
            this.LimitValues = limitValues;
        }

        public string Name { get; set; }
        public List<int> LimitValues { get; set; }
    }
}
