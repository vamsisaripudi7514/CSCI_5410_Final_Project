using Microsoft.AspNetCore.Mvc;

namespace AttendanceSystem.Controllers
{
    public class ClassesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
