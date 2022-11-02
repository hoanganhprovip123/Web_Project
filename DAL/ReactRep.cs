using WEB.DAL.Models;
using WEB.Repositories.DAL;

namespace WEB.DAL
{
    public class ReactReq : GenericSvc<SmDbContext, React>
    {
        public IQueryable<React> GetReactByPost(int postId)
        {
            return base.Get<React>(r => r.PostId == postId);
        }
        public IQueryable<React> GetReactByComment(int commentId)
        {
            return base.Get<React>(r => r.CommentId == commentId);
        }
        public React GetReactByPost(int postId, int userId)
        {
            return base.GetSingle<React>(r => r.PostId == postId && r.UserId == userId);
        }
        public React GetReactByComment(int commentId, int userId)
        {
            return base.GetSingle<React>(r => r.CommentId == commentId && r.UserId == userId);
        }
        public int CountReact(int month, int year)
        {
            int count;

            if(year == 0)
            {
                count = Context.Reacts.Count();
            }
            else if (month >=1 && month <= 12)
            {
                count = base.Get<React>(u => u.CreatedDate.Month == month || u.CreatedDate.Year == year).Count();
            }
            else
            {
                count = base.Get<React>(u => u.CreatedDate.Year == year).Count();
            }

            return count;
        }
    }
}
