using Microsoft.AspNetCore.Mvc;
using WEB.Services;

namespace WEB.Controllers
{
    [Route("user")]
    public class UserPageController : Controller
    {
        private UserService userService = new UserService();

        [Route("{userId}")]
        public IActionResult Userpage(int userId)
        {
            return View("user", userService.Get(userId));
        }
    }
}
