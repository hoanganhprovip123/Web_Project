using Microsoft.AspNetCore.Mvc;
using WEB.DAL.Models;
using WEB.Services;

namespace WEB.Controllers
{
    [Route("post")]
    public class PostController : Controller
    {
        private PostService postService = new PostService();
        private NotifyService notifyService = new NotifyService();

        [Route("{postId}")]
        public IActionResult PostPage([FromRoute] int postId, [FromQuery] int? notifyId, 
            [FromQuery] string? notifyType, [FromQuery(Name = "ref")] string? refer)
        {
            if(refer.Equals("Notify") && notifyId != null)
            {
                notifyService.readNotify((int)notifyId);
            }
            Post model = postService.Get(postId);
            return View("Post", model);
        }
    }
}
