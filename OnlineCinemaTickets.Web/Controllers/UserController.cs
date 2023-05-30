using Microsoft.AspNetCore.Mvc;

namespace OnlineCinemaTickets.Web.Controllers
{
    public class UserController : Controller
    {

        [HttpGet("[action]")]
        public IActionResult ImportUsers()
        {
            return View();
        }

    }
}
