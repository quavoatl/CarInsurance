using CarInsurance.ConstantsAndHelpers.Enums;
using CarInsurance.DataAccessV3.CarInsuranceDbContext;
using CarInsurance.DataAccessV3.DbModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace CarInsurance.DBServices.DbAddServices.BrokerDetailsService
{
    public class BrokerService : IBrokerService
    {

        private readonly CarInsuranceContextV3 _dbContext;

        public BrokerService(CarInsuranceContextV3 db)
        {
            _dbContext = db;
        }

        public bool CheckBrokerHasPolicyTemplate(AppUser user)
        {
            bool res = false;
            if (CheckIsBroker(user))
            {
                var brokerTemplate = _dbContext.BrokerPolicyTemplate.FirstOrDefault(t => t.BrokerReferenceId.Equals(Guid.Parse(user.Id)));
                if (brokerTemplate != null) res = true;
            }
            return res;
        }

        public bool CheckIsBroker(AppUser user)
        {
            if (user.IsBroker) return true;
            else return false;
        }

        public bool CreateBrokerPolicyTemplate(AppUser user)
        {
            int result = 0;
            if (CheckIsBroker(user) && !CheckBrokerHasPolicyTemplate(user))
            {
                var brokerTemplate = new BrokerPolicyTemplate()
                {
                    BrokerReferenceId = Guid.Parse(user.Id),
                    CarBrokerRefId = Guid.Parse(user.Id),
                    CoverBrokerRefId = Guid.Parse(user.Id),
                    CreationDate = DateTime.Now,
                    TemplateReady = false
                };

                _dbContext.BrokerPolicyTemplate.Add(brokerTemplate);
                result = _dbContext.SaveChanges();
            }
            return result == 1;
        }

        public ICollection<Car> GetCars(AppUser user)
        {
            ICollection<Car> listOfCars = null;
            bool brokerHasPolicyTemplate = CheckBrokerHasPolicyTemplate(user);

            if (brokerHasPolicyTemplate)
            {
                try
                {
                    listOfCars = _dbContext.Car.Where(car => car.CarBrokerRefId.Equals(Guid.Parse(user.Id.ToString()))).ToList();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.GetType());
                }
            }
            return listOfCars;
        }

        public ICollection<Cover> GetCovers(AppUser user)
        {
            ICollection<Cover> listOfCovers = null;
            bool brokerHasPolicyTemplate = CheckBrokerHasPolicyTemplate(user);

            if (brokerHasPolicyTemplate)
            {
                try
                {
                    listOfCovers = _dbContext.Cover.Where(cover => cover.CoverBrokerRefId.Equals(Guid.Parse(user.Id.ToString()))).ToList();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.GetType());
                }
            }
            else return new List<Cover>();

            return listOfCovers;
        }

        public Cover GetSpecificCover(CoverType coverType, AppUser user)
        {
            throw new NotImplementedException();
        }

        public BrokerPolicyTemplate RetrieveBrokerPolicyTemplate(AppUser user)
        {
            BrokerPolicyTemplate template = null;
            if (CheckIsBroker(user) && CheckBrokerHasPolicyTemplate(user))
            {
                template = _dbContext.BrokerPolicyTemplate.FirstOrDefault(t => t.BrokerReferenceId.Equals(Guid.Parse(user.Id)));
            }
            return template;
        }
    }
}
