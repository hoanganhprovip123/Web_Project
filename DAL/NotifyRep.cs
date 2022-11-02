using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer.Management.Smo.Agent;
using System.Data;
using WEB.DAL.Models;
using WEB.Repositories;
using WEB.Repositories.DAL;
using WEB.Repositories.Response;
using WEB.Repositories.Utils;

namespace WEB.DAL
{
    public class NotifyRep : GenericSvc<SmDbContext, Notify>
    {
        public List<NotifyResponse> getNotifies(int userId, int page)
        {
            List<NotifyResponse> rs = new List<NotifyResponse>();

            int size = Configs.NOTIFY_PAGE_SIZE;

            if (page > 0)
            {
                int start = (page - 1) * size;

                var command = base.Context.Database.GetDbConnection().CreateCommand();
                command.CommandText = $"EXEC sp_userGetNotifies {userId},{start},{size}";
                base.Context.Database.OpenConnection();

                using(var result = command.ExecuteReader())
                {
                    while (result.Read())
                    {
                        NotifyResponse r = new NotifyResponse();
                        r.TargetId = result.GetInt32("target_id");
                        r.TyPe = Enum.Parse<NotifyTypes>(result.GetString("type"));
                        r.IsRead = result.GetBoolean("is_read");
                        r.Count = result.GetInt32("count");
                        r.LastModifiedName = result.GetString("last_modify_name");
                        r.LastModifiedAvatar = result.GetString("last_modify_avatar");
                        r.LastModified = result.GetDateTime("last_modify");
                        r.NotifyId = result.GetInt32("notify_id");
                        rs.Add(r);
                    }
                }
            }
            return rs;
        }
    }
}
