using System.Linq.Expressions;
using WEB.Repositories.DAL;
using WEB.Repositories.Response;

namespace WEB.Repositories.BLL
{
    public class GenericSvc<D, T> : IGenericSvc<T> where T : class where D : IGenericRep<T>, new()
    {
        protected D _rep;
        public GenericSvc()
        {
            _rep = new D();
        }

        public IQueryable<T> All
        {
            get { return _rep.All; }
        }

        public SingleResponse Create(T entity)
        {
            var res = new SingleResponse();
            var now = DateTime.Now;

            _rep.Create(entity);

            return res;
        }

        public bool Delete(T obj)
        {
            return _rep.Delete(obj);
        }

        public SingleResponse Delete(string code)
        {
            return null;
        }

        public IQueryable<T> Get(Expression<Func<T, bool>> p)
        {
            return _rep.Get(p);
        }

        public virtual T Get<T>(int id) where T : TEntity
        {
            return _rep.GetSingle<T>(id);
        }

        public SingleResponse Get(string code)
        {
            return null;
        }

        public int Remove(int id)
        {
            return 0;
        }

        public SingleResponse Restore(int id)
        {
            return null;
        }

        public SingleResponse Restore(string code)
        {
            return null;
        }

        public SingleResponse Update(T entity)
        {
            var res = new SingleResponse();
            _rep.Update(entity);

            return res;
        }
    }
}
