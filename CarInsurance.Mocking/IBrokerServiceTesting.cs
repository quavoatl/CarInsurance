using CarInsurance.DataAccessV3.CarInsuranceDbContext;
using CarInsurance.DataAccessV3.DbModels;
using CarInsurance.DBServices.DbAddServices.BrokerDetailsService;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarInsurance.Mocking
{
    public class IBrokerServiceTesting
    {
        private readonly CarInsuranceContextV3 _dbContext;

        public IBrokerServiceTesting()
        {
            _dbContext = new CarInsuranceContextV3();
        }

        //SHOULD BE CHANGED 
        private readonly Guid _testedBrokerGuid = new Guid("f95f529d-7589-425d-96d8-db2d3b847ed5");

        [Test]
        public void CreateBrokerPolicyTemplate()
        {
            IBrokerService brokerService = new BrokerService(_dbContext);
            AppUser user = _dbContext.AppUser.FirstOrDefault(u => u.Id.Equals(_testedBrokerGuid.ToString()));

            bool result = brokerService.CreateBrokerPolicyTemplate(user);

            Assert.IsTrue(result);
        }

        [Test]
        public void RetrieveListOfCarsForBroker()
        {
            IBrokerService brokerService = new BrokerService(_dbContext);
            AppUser user = _dbContext.AppUser.FirstOrDefault(u => u.Id.Equals(_testedBrokerGuid.ToString()));

            var result = brokerService.GetCars(user);
        }
    }
}
