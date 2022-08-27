using eCommerce_Backend.Application.Interfaces;
using eCommerce_Backend.Data.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce_Backend.Application
{
    public class GenericRepository <T> : IGenericRepository <T> where T: class
    {
        protected readonly eCommerceDbContext dbContext;
        public GenericRepository(eCommerceDbContext context)
        {
            dbContext = context;
        }
        public void Add(T entity)
        {
            dbContext.Set<T>().Add(entity);
        }
        public void AddRange(IEnumerable<T> entities)
        {
            dbContext.Set<T>().AddRange(entities);
        }
        public IEnumerable<T> Find(Expression<Func<T, bool>> expression)
        {
            return dbContext.Set<T>().Where(expression);
        }
        public IEnumerable<T> GetAll()
        {
            return dbContext.Set<T>().ToList();
        }
        public T GetById(int id)
        {
            return dbContext.Set<T>().Find(id);
        }
        public void Remove(T entity)
        {
            dbContext.Set<T>().Remove(entity);
        }
        public void RemoveRange(IEnumerable<T> entities)
        {
            dbContext.Set<T>().RemoveRange(entities);
        }
    }
}
