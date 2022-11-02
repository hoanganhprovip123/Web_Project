using Microsoft.AspNetCore.Mvc;
using WEB.BLL;
using WEB.Services;

namespace WEB.Controllers
{
    [Route("admin")]
    public class AdminController1 : Controller
    {
        private AdminService adminService = new AdminService();
        private UserService userService = new UserService();

        [Route("")]
        public IActionResult Stats([FromQuery]int? month = 0, [FromQuery] int? year = 0)
        {
            return View("Stats", adminService.CountStats((int) month, (int) year));
        }

        [Route("user")]
        public IActionResult User([FromQuery] string? kw)
        {
            return View("User", userService.SearchByUser(kw, 0, 0));
        }
    }
}
