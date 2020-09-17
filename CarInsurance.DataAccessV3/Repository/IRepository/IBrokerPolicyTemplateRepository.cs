using CarInsurance.DataAccessV3.DbModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarInsurance.DataAccessV3.Repository.IRepository
{
    public interface IBrokerPolicyTemplateRepository : IRepository<BrokerPolicyTemplate>
    {
        void Update(BrokerPolicyTemplate template);

    }
}
