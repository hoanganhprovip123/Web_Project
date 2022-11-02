using WEB.DAL;
using WEB.DAL.Models;
using WEB.Repositories.BLL;
using WEB.Repositories.Request;

namespace WEB.Services
{
    public class PostService : GenericSvc<PostRep, Post>
    {
        private ReactService reactService;
        private CommentService commentService;
        public PostService()
        {
            reactService = new ReactService();
            commentService = new CommentService();
        }
        public Post Get(int id)
        {
            Post p = _rep.GetSingle<Post>(id);
            if(p != null)
            {
                p.User = base.Get<User>(p.UserId);
                p.Reacts = reactService.GetReactByPost(p.Id).ToHashSet();
                p.CommentSetLength = commentService.CountCommentByPost(p.Id);
            }

            return p;
        }
        
        public IQueryable<Post> Get(int page, string kw)
        {
            IQueryable<Post> rs = _rep.Get(page, kw);

            foreach(var p in rs)
            {
                p.User = base.Get<User>(p.UserId);
                p.Reacts = reactService.GetReactByPost(p.Id).ToHashSet();
                p.CommentSetLength = commentService.CountCommentByPost(p.Id);
            }

            return rs;
        }

        public Post Create(PostReq req, User creator)
        {
            Post p = new Post();

            p.Content = req.Content;
            p.Image = req.ImageUrl;
            p.UserId = creator.Id;
            p.Hashtag = req.Hashtag;

            if(_rep.Create(p) == true)
            {
                p.User = creator;
                return p;
            }

            return null;
        }

        public Post Update(int currentPostId, PostReq req, User creator)
        {
            Post currentPost = this.Get(currentPostId);

            currentPost.Content = req.Content;
            currentPost.Hashtag = req.Hashtag;

            if(currentPost.Image != req.ImageUrl && !String.IsNullOrEmpty(currentPost.Image))
            {
                var public_id = currentPost.Image.Substring(currentPost.Image.LastIndexOf("public_id=") + 10);
                currentPost.Image = req.ImageUrl;
            }

            if(_rep.Update(currentPost) == true)
            {
                return currentPost;
            }

            return null;
        }
        public bool Delete(int postId)
        {
            return base.Delete(base.Get<Post>(postId));
        }

        public Post SearchPostByComment(int commentId)  
        {
            return _rep.FindPostByComment(commentId);
        }

        public IQueryable<Post> SearchByContent(string kw, int page)
        {
            var p = _rep.Get(page, kw);
            return p;
        }

        public List<Post> SearchByHashtag(string hashtag, int page)
        {
            var posts = _rep.SearchByHashtag(hashtag, page).ToList();

            posts.ForEach(p =>
            {
                p.User = base.Get<User>(p.UserId);
                p.Reacts = reactService.GetReactByPost(p.Id).ToHashSet();
                p.CommentSetLength = commentService.CountCommentByPost(p.Id);
            });

            return posts;
        }

        public HashSet<Post> GetPostByUser(int userId, int page)
        {
            HashSet<Post> rs = _rep.GetPostByUser(userId, page);

            foreach(var post in rs)
            {
                post.User = base.Get<User>(post.UserId);
                post.Reacts = reactService.GetReactByPost(post.Id).ToHashSet();
                post.CommentSetLength = commentService.CountCommentByPost(post.Id);
            }

            return rs;
        }
    } 
}
