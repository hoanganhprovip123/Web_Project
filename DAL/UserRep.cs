using WEB.DAL.Models;
using WEB.Repositories;
using WEB.Repositories.DAL;

namespace WEB.DAL
{
    public class UserRep : GenericSvc<SmDbContext, User>
    {
        public User GetUserByPost(int postId)
        {
            var query = from user in Context.Users
                        join post in Context.Posts on user.Id equals post.UserId
                        where post.Id == postId
                        select user;
            return query.FirstOrDefault<User>();
        }
        public User GetUserByComment(int commentId)
        {
            var query = from user in Context.Users
                        join comment in Context.Comments on user.Id equals comment.UserId
                        where comment.Id == commentId
                        select user;
            return query.FirstOrDefault<User>();
        }
        public User GetUserByEmail(string email)
        {
            return base.Context.Set<User>().Where(u => u.Email == email).SingleOrDefault();
        }
        public int CountUser(int month, int year)
        {
            int count;
            if(year == 0)
            {
                count = Context.Users.Count();
            }
            else if (month >= 1 && month <= 12)
            {
                count = Context.Users.Where(u => u.CreatedDate.Month == month && u.CreatedDate.Year == year).Count();
            }
            else
            {
                count = Context.Users.Where(u => u.CreatedDate.Year == year).Count();
            }
            return count;
        }

        public List<User> SearchByUser(string kw, int page, int limit)
        {
            List<User> rs = new List<User>();
            int size = limit == 0 ? Configs.POST_PAGE_SIZE : limit;
            if(kw == null)
            {
                kw = "";
            }
            if(page > 0)
            {
                int start = (page - 1) * size;
                rs = base.Context.Set<User>().Where(u => u.FirstName.Contains(kw) 
                || u.LastName.Contains(kw)).AsEnumerable().Skip(start).Take(size).ToList();
            }
            else
            {
                rs = base.Context.Set<User>().Where(u => u.FirstName.Contains(kw) || u.LastName.Contains(kw)).ToList();
            }

            return rs;
        }
    }
}
