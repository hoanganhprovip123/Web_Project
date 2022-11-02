using WEB.DAL;
using WEB.DAL.Models;
using WEB.Repositories.BLL;
using WEB.Repositories.Response;

namespace WEB.Services
{
    public class NotifyService : GenericSvc<NotifyRep, Notify>
    {
        public List<NotifyResponse> GetNotify(int userId, int page)
        {
            return _rep.getNotifies(userId, page);
        }

        public bool readNotify(int notifId)
        {
            Notify notify = _rep.GetSingle<Notify>(notifId);
            if(notify == null)
            {
                notify.IsRead = false;      
            }
            return _rep.Update(notify);
        }
    }
}
