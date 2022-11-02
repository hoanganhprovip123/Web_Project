using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace WEB.Repositories.DAL
{
    public class GenericSvc<C, T> : IGenericRep<T> where T : class where C : DbContext, new()
    {
        public C Context
        {
            get { return _context; }
            set { _context = value; }
        }
        private C _context;
        public GenericSvc()
        {
            _context = new C();
        }
        public IQueryable<T> All
        {
            get
            {
                return _context.Set<T>();
            }
        }

        public bool Create(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();

            return true;
        }

        public bool Create(List<T> entities)
        {
            _context.Set<T>().AddRange(entities);
            _context.SaveChanges();

            return true;
        }

        public bool Delete(T entity)
        {
            if(entity == null)
            {
                return true;
            }
            var t = _context.Set<T>().Remove(entity);
            return true;
        }

        public IQueryable<T> Get<T>(Expression<Func<T, bool>> p) where T : class
        {
            return _context.Set<T>().Where(p);
        }

        public virtual T Get(string code)
        {
            return null;
        }

        public virtual T GetSingle<T>(int id) where T : TEntity
        {
            return _context.Set<T>().Where(p => p.Id == id).SingleOrDefault();
        }
        public virtual T GetSingle<T>(Expression<Func<T, bool>> p)where T : class
        {
            return _context.Set<T>().Where(p).SingleOrDefault();
        }

        public bool Update(T entity)
        {
            _context.Set<T>().Update(entity);
            _context.SaveChanges();

            return true;
        }

        public bool Update(List<T> entities)
        {
            _context.Set<T>().UpdateRange(entities);
            _context.SaveChanges();

            return true;
        }
    }
}
