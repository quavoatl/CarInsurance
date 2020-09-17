using CarInsurance.DataAccessV3.CarInsuranceDbContext;
using CarInsurance.DataAccessV3.DbModels;
using CarInsurance.DBServices.DbAddServices.BrokerDetailsService;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarInsurance.Mocking
{
    public class DatabaseTesting
    {
        private readonly CarInsuranceContextV3 _dbContext;

        public DatabaseTesting()
        {
            _dbContext = new CarInsuranceContextV3();
        }

        //SHOULD BE CHANGED 
        private readonly Guid _testedBrokerGuid = new Guid("b3f84ca1-0142-4aae-835a-4c6fff187303");

        [Test]
        //[Ignore("Outdated")]
        public void AddingLimitAndCheckingItBack()
        {

            using (var _dbContext = new CarInsuranceContextV3())
            {
                var brokerTemplate = new BrokerPolicyTemplate()
                {
                    BrokerReferenceId = _testedBrokerGuid,
                    CarBrokerRefId = _testedBrokerGuid,
                    CoverBrokerRefId = _testedBrokerGuid,
                    CreationDate = DateTime.Now,
                    TemplateReady = false,
                };

                _dbContext.BrokerPolicyTemplate.Add(brokerTemplate);
                _dbContext.SaveChanges();

                var coverAccidents = new Cover()
                {
                    CoverBrokerRefId = _testedBrokerGuid,
                    AdditionDate = DateTime.Now,
                    LimitCoverId = _testedBrokerGuid,
                    QuestionCoverId = _testedBrokerGuid,
                    Type = "accidents"
                };

                _dbContext.Cover.Add(coverAccidents);
                _dbContext.SaveChanges();

                Limit limit = new Limit
                {
                    LimitCoverId = _testedBrokerGuid,
                    CoverType = "accidents",
                    LimitRuleCoverId = _testedBrokerGuid,
                    LimitValues = "0,100,500,1000",
                    Name = "AccidentsLimit"
                };

                _dbContext.Limit.Add(limit);
                var res = _dbContext.SaveChanges();

                Assert.AreEqual(res, 1);

                var retrievedLimit = _dbContext.Limit.Where(pk => pk.LimitCoverId.Equals(_testedBrokerGuid)).FirstOrDefault();

                Assert.IsTrue(retrievedLimit.CoverType.Equals("accidents"));
            }
        }

        [Test]
        public void UpdateLimitOnAccidentsCover()
        {
            using (var _dbContext = new CarInsuranceContextV3())
            {
                var limitRetrieved = _dbContext.Limit.Where(pk => pk.LimitCoverId.Equals(_testedBrokerGuid)).FirstOrDefault();
                var listOfLimitValues = new List<int>();
                foreach (var limit in limitRetrieved.LimitValues.Split(',', StringSplitOptions.RemoveEmptyEntries))
                {
                    listOfLimitValues.Add(int.Parse(limit));
                }
                listOfLimitValues.Add(2000);

                var newLimitValues = string.Empty;
                foreach (var limit in listOfLimitValues)
                {
                    newLimitValues += limit.ToString() + ",";
                }

                limitRetrieved.LimitValues = newLimitValues;
                _dbContext.SaveChanges();

                var limitRetrieved2 = _dbContext.Limit.Where(pk => pk.LimitCoverId.Equals(_testedBrokerGuid)).FirstOrDefault();

                Assert.AreEqual(limitRetrieved2.LimitValues.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList().Last(), "2000");

            }
        }

        [Test]
        public void CoverAdditionCoverTypeConstraints()
        {
            using (var _dbContext = new CarInsuranceContextV3())
            {
                var testCover = new Cover()
                {
                    CoverBrokerRefId = _testedBrokerGuid,
                    AdditionDate = DateTime.Now,
                    LimitCoverId = Guid.NewGuid(),
                    QuestionCoverId = Guid.NewGuid(),
                    Type = "test"
                };

                try
                {
                    _dbContext.Cover.Add(testCover);
                    _dbContext.SaveChanges();
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
        }

        [Test]
        public void AddGoodCover()
        {
            using (var _dbContext = new CarInsuranceContextV3())
            {
                var testCover = new Cover()
                {
                    CoverBrokerRefId = _testedBrokerGuid,
                    AdditionDate = DateTime.Now,
                    LimitCoverId = Guid.NewGuid(),
                    QuestionCoverId = Guid.NewGuid(),
                    Type = "theft"
                };

                _dbContext.Cover.Add(testCover);
                var result = _dbContext.SaveChanges();
                Assert.AreEqual(result, 1);
            }
        }

        [Test]
        public void AddGoodCar()
        {
            using (var _dbContext = new CarInsuranceContextV3())
            {
                var car = new Car()
                {
                    CarBrokerRefId = _testedBrokerGuid,
                    Brand = "Opel",
                    EngineCC = 1600,
                    EuroStandard = "euro4",
                    Model = "astra",
                    Year = DateTime.Parse("05/06/2008"),
                    CarRuleCoverId = Guid.NewGuid()
                };

                _dbContext.Car.Add(car);
                var result = _dbContext.SaveChanges();
                Assert.AreEqual(result, 1);
            }
        }




        [Test]
        public void AddLimitForCover()
        {
            using (var _dbContext = new CarInsuranceContextV3())
            {
                var coverRetrieved = _dbContext.Cover.Where(c => c.CoverBrokerRefId.Equals(_testedBrokerGuid) && c.Type.Equals("theft")).FirstOrDefault();

                var newLimit = new Limit()
                {
                    CoverType = coverRetrieved.Type,
                    LimitCoverId = coverRetrieved.LimitCoverId,
                    LimitRuleCoverId = Guid.NewGuid(),
                    Name = "TheftLimit",
                    LimitValues = "0,100,1000"
                };

                _dbContext.Limit.Add(newLimit);
                _dbContext.SaveChanges();

            }
        }

        [Test]
        public void AddLimitRuleForLimit()
        {
            using (var _dbContext = new CarInsuranceContextV3())
            {
                var coverRetrieved = _dbContext.Cover.Where(c => c.CoverBrokerRefId.Equals(_testedBrokerGuid) && c.Type.Equals("theft"))
                    .Include(l => l.Limit)
                    .FirstOrDefault();


                var newLimitRule1 = new LimitRule()
                {
                    LimitRuleCoverId = coverRetrieved.Limit.FirstOrDefault().LimitRuleCoverId,
                    Name = "Equal500",
                    RuleText = "EqualTo500",
                    Multiplier = 0.6f,
                };

                var newLimitRule2 = new LimitRule()
                {
                    LimitRuleCoverId = coverRetrieved.Limit.FirstOrDefault().LimitRuleCoverId,
                    Name = "Equal1000",
                    RuleText = "EqualTo1000",
                    Multiplier = 0.8f,
                };

                _dbContext.LimitRule.AddRange(newLimitRule1, newLimitRule2);
                _dbContext.SaveChanges();

            }
        }

        [Test]
        public void RetrieveCover()
        {
            using (var _dbContext = new CarInsuranceContextV3())
            {
                var coverRetrieved = _dbContext.Cover.Where(c => c.CoverBrokerRefId.Equals(_testedBrokerGuid) && c.Type.Equals("naturalhazard"))
                    .Include(c => c.Limit).ThenInclude(l => l.LimitRule).FirstOrDefault();






            }
        }
    }
}
