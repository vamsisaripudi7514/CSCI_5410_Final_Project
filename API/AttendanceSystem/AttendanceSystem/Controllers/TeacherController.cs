using AttendanceSystem.DTOs.Teacher;
using AttendanceSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AttendanceSystem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TeacherController : Controller
    {
        private readonly AttendanceSystemContext _context;
        public TeacherController(AttendanceSystemContext context)
        {
            _context = context;
        }

        [HttpPost("get-teacher-classes")]
        public async Task<IActionResult> GetTeacherClasses([FromBody] GetTeacherClasses data)
        {
            if (data==null || data.TeacherID == null)
            {
                return BadRequest("Invalid Teacher ID");
            }
            var teacher_data = await _context.Usermasters
                                      .FirstOrDefaultAsync(c => c.MasterId == data.TeacherID && c.Role == "teacher");
            if (teacher_data == null)
            {
                return BadRequest("The given user is not a Teacher");
            }
            var classes = _context.Classes
                .Where(c => c.TeacherId == data.TeacherID)
                .ToList();
            if (classes == null || classes.Count == 0)
            {
                return NotFound("No classes found for this teacher.");
            }
            return Ok(classes);
        }
    }
}
