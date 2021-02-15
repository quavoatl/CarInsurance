using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CarInsurance.DataAccessV2.DbModels;

namespace CarInsurance.DataAccessV2.Repositories.Cover
{
    public class CoverRepository : ICoverRepository
    {
        private readonly CarInsuranceV3Context _dbContext;

        public CoverRepository(CarInsuranceV3Context dbContext)
        {
            _dbContext = dbContext;
        }

        public List<DbModels.Cover> GetCovers()
        {
            return _dbContext.Covers.ToList();
        }

        public DbModels.Cover GetCover(string guid)
        {
            return _dbContext.Covers.Where(c => c.CoverBrokerRefId.Equals(Guid.Parse(guid))).FirstOrDefault();
        }

        public void UpdateCover(DbModels.Cover cover)
        {
            var coverFromDb = _dbContext.Covers.Where(c => c.CoverId.Equals(cover.CoverId)).FirstOrDefault();
            coverFromDb.AdditionDate = cover.AdditionDate;
            _dbContext.SaveChanges();
        }

        public void Insert(DbModels.Cover cover)
        {
            _dbContext.Add(cover);
            _dbContext.SaveChanges();
        }
    }
}
