using Microsoft.AspNetCore.Mvc;
using Microsoft.SqlServer.Management.Smo;
using Newtonsoft.Json;
using WEB.DAL.Models;
using WEB.Repositories.Request;
using WEB.Services;

namespace WEB.Controllers.API
{
    [Route("api/comment")]
    [ApiController]
    public class APICommentController : ControllerBase
    {
        private CommentService commentService = new CommentService();

        // GET: api/comment?params
        [HttpGet]
        public IActionResult GetCommentByPost(int postId, int? page = 0)
        {
            return Ok(commentService.GetCommentByPost(postId, page));
        }

        [HttpGet("get-replies")]
        public IActionResult GetCommentReplies(int commentId, int? page = 0)
        {
            return Ok(commentService.GetCommentReplies(commentId, page));   
        }

        [HttpGet("{id}")]
        public IActionResult GetComment(int id)
        {
            return Ok(commentService.GetComment(id));
        }

        // POST api/comment/add
        //[HttpPost("add")]
        //public IActionResult Post([FromBody] CommentReq req)
        //{
        //    User currentUser = new User();
        //    if (HttpContext.Session.GetString("currentUser") != null)
        //    {
        //        currentUser = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("currentUser"));
        //        Comment res = commentService.Create(req, currentUser);
        //        if (res != null)
        //        {
        //            if (res.Post != null && res.Post.User != null)
        //                NotificationCenter.SendMessage("update_notif", res.Post.User.Uuid.Trim());
        //            else if (res.Parent != null && res.Parent.User != null)
        //                NotificationCenter.SendMessage("update_notif", res.Parent.User.Uuid.Trim());
        //            return Ok(res);
        //        }
        //    }

        //    return StatusCode(500);
        //}
    }
}
