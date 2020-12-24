using CarInsurance.DataAccessV3.CarInsuranceDbContext;
using CarInsurance.DataAccessV3.DbModels;
using CarInsurance.DataAccessV3.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarInsurance.DataAccessV3.Repository.DbModelsRepositories
{
    public class CoverRepository : Repository<Cover>, ICoverRepository
    {
        private readonly CarInsuranceContextV3 _dbContext;

        public CoverRepository(CarInsuranceContextV3 db) : base(db)
        {
            _dbContext = db;
        }

        public void Update(Cover cover)
        {
            var coverFromDb = _dbContext.Cover.FirstOrDefault(c => c.CoverBrokerRefId.Equals(cover.CoverBrokerRefId));

            if (coverFromDb != null)
            {
                coverFromDb.Limit = cover.Limit;
                coverFromDb.Question = cover.Question;

                _dbContext.SaveChanges();
            }
        }
    }
}
