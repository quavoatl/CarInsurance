using CarInsurance.DataAccessV3.CarInsuranceDbContext;
using CarInsurance.DataAccessV3.DbModels;
using CarInsurance.DataAccessV3.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarInsurance.DataAccessV3.Repository.DbModelsRepositories
{
    public class BrokerPolicyTemplateRepository : Repository<BrokerPolicyTemplate>, IBrokerPolicyTemplateRepository
    {
        private readonly CarInsuranceContextV3 _dbContext;

        public BrokerPolicyTemplateRepository(CarInsuranceContextV3 db) : base(db)
        {
            _dbContext = db;
        }

        public void Update(BrokerPolicyTemplate template)
        {
            var templateFromDb = _dbContext.BrokerPolicyTemplate.FirstOrDefault(t => t.CoverBrokerRefId.Equals(template.CoverBrokerRefId));
            if (templateFromDb != null)
            {
                templateFromDb.TemplateReady = template.TemplateReady;
                _dbContext.SaveChanges();
            }
        }
    }
}
