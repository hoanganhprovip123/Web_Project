using WEB.DAL.Models;
using WEB.Repositories;
using WEB.Repositories.DAL;

namespace WEB.DAL
{
    public class CommentRep : GenericSvc<SmDbContext, Comment>
    {
        public IQueryable<Comment> GetCommentsByPost(int postId, int? page)
        {
            IQueryable<Comment> rs;
            int size = Configs.COMMENT_PAGE_SIZE;
            if(page > 0)
            {
                int start = ((int) page -1) * size;
                rs = base.Get<Comment>(c => c.PostId == postId).AsEnumerable().Skip(start).Take(size).AsQueryable();
            }
            else
            {
                rs = base.Get<Comment>(c => c.PostId == postId);
            }
            return rs;
        }
        public int CountCommentByPost(int postId)
        {
            return base.Get<Comment>(c => c.PostId == postId).Count();
        }
        public int CountReplies(int commentId)
        {
            return base.Get<Comment>(c => c.ParentId == commentId).Count();
        }
        public IQueryable<Comment> GetCommentReplies(int commentId, int? page)
        {
            IQueryable<Comment> rs;
            int size = Configs.COMMENT_PAGE_SIZE;
            if(page > 0)
            {
                int start = ((int)page - 1) * size;

                rs = base.Get<Comment>(c => c.ParentId == commentId).AsEnumerable().Skip(start).Take(size).AsQueryable();
            }
            else
            {
                rs = base.Get<Comment>(c => c.ParentId == commentId);
            }
            return rs;
        }
        public int CountComment(int month, int year)
        {
            int count;
            if(year == 0)
            {
                count = base.All.Count();
            }
            else if(month >= 1 && month <= 12)
            {
                count = base.Get<Comment>(u => u.CreatedDate.Month == month || u.CreatedDate.Year == year).Count();
            }
            else
            {
                count = base.Get<Comment>(u => u.CreatedDate.Year == year).Count();
            }
            return count;
        }
    }
}
