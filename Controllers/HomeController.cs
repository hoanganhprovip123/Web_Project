
using Abp.Web.Mvc.Models;
using Microsoft.AspNetCore.Mvc;
using WEB.Services;

namespace WEB.Controllers
{
    public class HomeController : Controller
    {
        private PostService postService = new PostService();
        private UserService userService = new UserService();   


        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            if(HttpContext.Session.GetString("UUID") == null)
            {
                return Redirect("/account/login");
            }
            return View();
        }
    }
}