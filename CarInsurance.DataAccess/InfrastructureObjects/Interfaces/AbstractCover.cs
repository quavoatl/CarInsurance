using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using CarInsurance.DataAccess.ModelsPOCOs;

namespace CarInsurance.DataAccess.InfrastructureObjects.Interfaces
{
    public abstract class AbstractCover
    {

       
        public virtual Limit Limit { get; set; }
        public virtual IEnumerable<Question> Questions { get; set; }
       
    }
}
