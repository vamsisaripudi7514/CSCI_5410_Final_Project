using Microsoft.AspNetCore.Mvc;

namespace AttendanceSystem.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
