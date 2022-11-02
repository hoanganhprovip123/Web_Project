using WEB.DAL;
using WEB.DAL.Models;
using WEB.Repositories.BLL;
using WEB.Repositories.Request;
using BC = BCrypt.Net.BCrypt;

namespace WEB.Services
{
    public class UserService : GenericSvc<UserRep, User>
    {
        public User Authenticate(string email, string password)
        {
            User user = _rep.GetUserByEmail(email.Trim());

            if(user != null && BC.Verify(password.Trim(), user.Password.Trim()))
            {
                return user;
            }
            else
            {
                return null;
            }
        }
        public bool UserRegistration(UserReq req)
        {
            User user = new User(req);
            Guid uuid = Guid.NewGuid();
            user.Uuid = uuid.ToString();
            user.UserRole = "ROLE_USER";
            user.CreatedDate = DateTime.Now;
            if (String.IsNullOrEmpty(user.Avatar))
            {
                user.Avatar = "https://res.cloudinary.com/dynupxxry/image/upload/v1660532211/non-avatar_nw91c3.png";
            }

            return _rep.Create(user);
        }
        public User GetUserByPost(int postId)
        {
            return _rep.GetUserByPost(postId);
        }

        public User GetUserByComment(int commentId)
        {
            return _rep.GetUserByComment(commentId);
        }

        public List<User> SearchByUser(string kw, int page, int limit)
        {
            var users = _rep.SearchByUser(kw, page, limit).ToList();


            return users;
        }
        public User Get(int id)
        {
            return base.Get<User>(id);
        }

        public bool Delete(int id)
        {
            return base.Delete(base.Get<User>(id));
        }
    }
}
