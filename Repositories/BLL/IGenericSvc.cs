using System.Linq.Expressions;
using WEB.Repositories.DAL;
using WEB.Repositories.Response;

namespace WEB.Repositories.BLL
{
    public interface IGenericSvc<T> where T : class
    {
        IQueryable<T> All { get; }
        SingleResponse Create(T entity);
        IQueryable<T> Get(Expression<Func<T, bool>> p);
        T Get<T>(int id) where T : TEntity;
        SingleResponse Get(string code);
        SingleResponse Update(T entity);
        bool Delete(T obj);
        SingleResponse Delete(string code);
        SingleResponse Restore(int id);
        SingleResponse Restore(string code);
        int Remove(int id);
    }
}
