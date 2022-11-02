using System.Linq.Expressions;

namespace WEB.Repositories.DAL
{
    public interface IGenericRep<T> where T : class
    {
        IQueryable<T> All { get; }
        bool Create(T entity);
        bool Create(List<T> entities);
        bool Update(T entity);
        bool Update(List<T> entities);
        bool Delete(T entity);
        IQueryable<T> Get<T>(Expression<Func<T, bool>> p) where T : class;
        T GetSingle<T>(int id) where T : TEntity;
        T Get(string code);
    }
}
