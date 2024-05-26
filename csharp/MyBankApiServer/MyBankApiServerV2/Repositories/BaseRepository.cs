using Microsoft.EntityFrameworkCore;
using MyBankApiServerV2.Models;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;

namespace MyBankApiServerV2.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected AppDbContext AppDbCxt { get; set; }

        public BaseRepository(AppDbContext appDbContext)
        {
            AppDbCxt = appDbContext;
        }

        public void Create(T entity)
        {
            AppDbCxt.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            AppDbCxt.Set<T>().Remove(entity);
        }

        public IQueryable<T> FindAll()
        {
            return AppDbCxt.Set<T>().AsNoTracking();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return AppDbCxt.Set<T>().Where(expression).AsNoTracking();
        }

        public void Update(T entity)
        {
            AppDbCxt.Set<T>().Update(entity);
        }
    }
}
