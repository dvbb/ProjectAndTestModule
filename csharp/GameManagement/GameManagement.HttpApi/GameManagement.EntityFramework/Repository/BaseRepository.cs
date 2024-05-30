using GameManagement.Contract.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GameManagement.EntityFramework.Repository
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected GameManagementDbContext GameDbContext { get; set; }
        protected BaseRepository(GameManagementDbContext context)
        {
            GameDbContext = context;
        }

        public void Create(T entity)
        {
            GameDbContext.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            GameDbContext.Set<T>().Remove(entity);
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return GameDbContext.Set<T>().Where(expression).AsNoTracking();
        }

        public void Update(T entity)
        {
            GameDbContext.Set<T>().Update(entity);
        }

        public IQueryable<T> FindALL()
        {
            return GameDbContext.Set<T>().AsNoTracking();
        }
    }
}
