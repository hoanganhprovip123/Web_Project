using Microsoft.AspNetCore.Mvc;

namespace WEB.Controllers
{
    [Route("account")]
    public class AccountController : Controller
    {
        [Route("login")]
        public IActionResult Login()
        {
            if(HttpContext.Session.GetString("UUID") != null)
            {
                return Redirect("/");
            }

            return View("Login");
        }

        [Route("register")]
        public IActionResult Register()
        {
            return View("Reigster");
        }
        [Route("Logout")]
        public IActionResult Logout()
        {
            foreach (var cookie in Request.Cookies.Keys)
            {
                if(cookie == "SocialMedia")
                {
                    Response.Cookies.Delete(cookie);
                }
            }
            HttpContext.Session.Clear();

            return Redirect("/account/login");
        }
    }
}
