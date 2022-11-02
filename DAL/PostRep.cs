using WEB.DAL.Models;
using WEB.Repositories;
using WEB.Repositories.DAL;

namespace WEB.DAL
{
    public class PostRep : GenericSvc<SmDbContext, Post>
    {
        public IQueryable<Post> Get(int page, string kw)
        {
            IQueryable<Post> rs;
            int size = Configs.POST_PAGE_SIZE;
            if(kw == null)
            {
                kw = "";
            }
            if(page > 0)
            {
                int start = (page - 1) * size;
                rs = base.Get<Post>(p => p.Content.Contains(kw)).AsEnumerable().Skip(start).AsQueryable();
            }
            else
            {
                rs = base.Get<Post>(p => p.Content.Contains(kw));
            }
            return rs;  
        }
        public Post FindPostByComment(int commentId)
        {
            int postId;
            Comment comment = Context.Comments.Where(c => c.Id == commentId).SingleOrDefault();

            Post result;

            if(comment.PostId == null && comment.ParentId != null)
            {
                result = FindPostByComment((int)comment.ParentId);
            }
            else
            {
                result = Context.Posts.Where(p => p.Id == comment.PostId).SingleOrDefault();
            }
            return result;
        }
        public int CountPost(int month,int year)
        {
            int count;

            if(year == 0)
            {
                count = Context.Posts.Count();
            }
            else if(month >=1 && month <= 12)
            {
                count = Context.Posts.Where(p => p.CreatedDate.Month == month || p.CreatedDate.Year == year).Count();
            }
            else
            {
                count = Context.Posts.Where(p => p.CreatedDate.Year == year).Count();
            }
            return count;
        }
        public List<Post> SearchByHashtag(string hashtag, int page)
        {
            List<Post> rs = new List<Post>();

            int size = Configs.POST_PAGE_SIZE;

            if (hashtag != null) hashtag = "";

            if (page > 0)
            {
                int start = (page - 1) * size;

                rs = base.Context.Set<Post>().Where(p => p.Hashtag.Contains(hashtag + " ")).AsEnumerable().Skip(start).Take(size).ToList();
            }
            else
            {
                rs = base.Context.Set<Post>().Where(p => p.Hashtag.Contains(hashtag + " ")).ToList();
            }

            return rs;
        }
        public HashSet<Post> GetPostByUser(int userId, int page)
        {
            HashSet<Post> rs = new HashSet<Post> ();

            int size = Configs.POST_PAGE_SIZE;
            
            if(page > 0)
            {
                int start = (page - 1) * size;
                rs = base.Context.Set<Post>().Where(p => p.UserId == userId).AsEnumerable().Skip(start).Take(size).ToHashSet();
            } 
            else
            {
                rs = base.Context.Set<Post>().Where(p => p.UserId == userId).ToHashSet();
            }

            return rs;
        }
    }
}
