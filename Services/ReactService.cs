using WEB.DAL;
using WEB.DAL.Models;
using WEB.Repositories.BLL;

namespace WEB.Services
{
    public class ReactService : GenericSvc<ReactReq, React>
    {
        private UserService userService;
        public ReactService()
        {
            userService = new UserService();    
        }
        public IQueryable<React> GetReactByPost(int postId)
        {
            IQueryable<React> rs = _rep.GetReactByPost(postId);

            foreach(var r in rs)
            {
                r.User = base.Get<User>(r.UserId);
            }

            return rs;
        }
        public IQueryable<React> GetReactByComment(int commentId)
        {
            IQueryable<React> rs = _rep.GetReactByComment(commentId);

            foreach(var r in rs)
            {
                r.User = base.Get<User>(r.UserId);
            }

            return rs;
        }

        public User CreateReact(int? postId, int? commentId, int userId)
        {
            React react = new React();

            react.UserId = userId;
            react.PostId = postId;
            react.CommentId = commentId;
            react.CreatedDate = DateTime.Now;
            react.Type = 1;

            if (_rep.Create(react))
            {
                if(postId != null)
                {
                    return userService.GetUserByPost((int)postId);
                }
                else
                {
                    return userService.GetUserByComment((int)commentId);
                }
            }

            return null;
        }
        
        public bool DeleteReact(int? postId, int? commentId, int userId)
        {
            React react;

            if(postId != null)
            {
                react = _rep.GetReactByPost((int)postId, userId);
            }
            else
            {
                react = _rep.GetReactByComment((int)commentId, userId);
            }

            return _rep.Delete(react);
        }
    }
}
