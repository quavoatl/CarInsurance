using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace CarInsurance.DataAccessV3.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        ICollection<T> GetAll(Expression<Func<T, bool>> filter = null,Func<IQueryable<T>,IOrderedQueryable<T>> orderBy = null, string includeProps = null);
        T GetFirstOrDefault(Expression<Func<T, bool>> filter = null, string includeProps = null);
        T Add(T entity);
        ICollection<T> AddRange(ICollection<T> entities);
        void Remove(T entity);
        void RemovRange(ICollection<T> entities);


    }
}
