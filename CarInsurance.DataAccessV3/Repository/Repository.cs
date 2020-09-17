using CarInsurance.DataAccessV3.CarInsuranceDbContext;
using CarInsurance.DataAccessV3.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace CarInsurance.DataAccessV3.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly CarInsuranceContextV3 _dbContext;
        internal DbSet<T> dbSet;

        public Repository(CarInsuranceContextV3 dbContext)
        {
            _dbContext = dbContext;
            dbSet = _dbContext.Set<T>();
        }

        public T Add(T entity)
        {
            _dbContext.Add(entity);
            return entity;
        }

        public ICollection<T> AddRange(ICollection<T> entities)
        {
            _dbContext.AddRange(entities);
            return entities;
        }

        public ICollection<T> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProps = null)
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProps != null)
            {
                foreach (var prop in includeProps.Split(',', StringSplitOptions.RemoveEmptyEntries))
                {
                    query.Include(prop);
                }
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }

            return query.ToList();
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> filter = null, string includeProps = null)
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProps != null)
            {
                foreach (var prop in includeProps.Split(',', StringSplitOptions.RemoveEmptyEntries))
                {
                    query.Include(prop);
                }
            }

            return query.FirstOrDefault();
        }

        public void Remove(T entity)
        {
            _dbContext.Remove(entity);
        }

        public void RemovRange(ICollection<T> entities)
        {
            _dbContext.RemoveRange(entities);
        }
    }
}
