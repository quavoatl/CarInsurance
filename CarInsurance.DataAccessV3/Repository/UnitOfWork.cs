using CarInsurance.DataAccessV3.CarInsuranceDbContext;
using CarInsurance.DataAccessV3.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarInsurance.DataAccessV3.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CarInsuranceContextV3 _dbContext;

        public UnitOfWork(CarInsuranceContextV3 db) 
        {
            _dbContext = db;
        }

        public IBrokerPolicyTemplateRepository BrokerPolicyTemplateRepository { get; private set; }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public int Save()
        {
            return _dbContext.SaveChanges();
        }
    }
}
