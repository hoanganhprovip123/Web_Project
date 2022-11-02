using System.Diagnostics;
using WEB.DAL;
using WEB.DAL.Models;
using WEB.Repositories.BLL;
using WEB.Repositories.Request;

namespace WEB.Services
{
    public class CommentService : GenericSvc<CommentRep, Comment>
    {
        private UserService userService;
        private ReactService reactService;

        public CommentService()
        {
            userService = new UserService();
            reactService = new ReactService();
        }
        public Comment GetComment(int id)
        {
            Comment comment = _rep.GetSingle<Comment>(id);

            comment.User = base.Get<User>(comment.UserId);
            comment.Reacts = reactService.GetReactByComment(comment.Id).ToHashSet();
            comment.CommentSetLength = _rep.CountReplies(comment.Id);

            if(comment.ParentId != null)
            {
                comment.Parent = this.GetComment((int)comment.ParentId);
            }

            return comment; 
        }
        
        public IQueryable<Comment> GetCommentByPost(int postId, int? page)
        {
            IQueryable<Comment> rs = _rep.GetCommentsByPost(postId, page);
            
            foreach(var c in rs)
            {
                c.User = base.Get<User>(c.UserId);
                c.Reacts = reactService.GetReactByComment(c.Id).ToHashSet();
                c.CommentSetLength = _rep.CountReplies(c.Id);
            }

            return rs;  
        }

        public int CountCommentByPost(int postId)
        {
            return _rep.CountCommentByPost(postId);
        }

        public IQueryable<Comment> GetCommentReplies(int commentId, int? page)
        {
            IQueryable<Comment> rs = _rep.GetCommentReplies(commentId, page);
            foreach (var c in rs)
            {
                c.User = base.Get<User>(c.UserId);
                c.Reacts = reactService.GetReactByComment(c.Id).ToHashSet();
                c.CommentSetLength = _rep.CountReplies(c.Id);
            }

            return rs;
        }
        public bool Delete(int id)
        {
            return base.Delete(base.Get<Comment>(id));
        }
        public Comment Create(CommentReq req, User creator)
        {
            try
            {
                Comment c = new Comment();

                c.Content = req.Content;
                c.CreatedDate = DateTime.Now;
                c.UserId = creator.Id;
                c.PostId = req.PostId;
                c.ParentId = req.CommentId;

                if(_rep.Create(c) == true)
                {
                    if(c.PostId != null)
                    {
                        c.Post = new Post();
                        c.Post.User = userService.GetUserByPost((int)c.PostId);
                    }
                    else
                    {
                        c.Parent = new Comment();
                        c.Parent.User = userService.GetUserByComment((int)c.ParentId);
                    }
                    
                    c.User = creator;
                    return c;
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }

            return null;
        }

        public Comment Update(int currentCommentId, CommentReq req)
        {
            Comment currentComment = this.GetComment(currentCommentId);

            currentComment.User = base.Get<User>(currentComment.UserId);
            currentComment.Reacts = reactService.GetReactByComment(currentComment.Id).ToHashSet();
            currentComment.Content = req.Content;
            currentComment.CreatedDate = DateTime.Now;

            if(_rep.Update(currentComment) == true)
            {
                return currentComment;
            }
            return null;
        }
    }
}
