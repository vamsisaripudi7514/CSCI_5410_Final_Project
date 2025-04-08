using Microsoft.AspNetCore.Mvc;

namespace AttendanceSystem.Controllers
{
    public class AttendanceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
