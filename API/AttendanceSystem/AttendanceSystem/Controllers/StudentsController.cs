using AttendanceSystem.DTOs.Attendance;
using AttendanceSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AttendanceSystem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentsController : Controller
    {
        private readonly AttendanceSystemContext _context;
        public StudentsController(AttendanceSystemContext context)
        {
            _context = context;
        }

        [HttpGet("get-students")]
        public async Task<IActionResult> GetStudents()
        {
            var query = from o in  _context.Users
                        where o.Role == "Student"
                        select new
                        {
                            user_id = o.UserId,
                            username = o.Username
                        };
            var students = query.ToList();
            if (students == null)
            {
                return NotFound();
            }
            return Ok(students);

        }

        [HttpPost("get-student-classes")]
        public async Task<IActionResult> GetStudentClasses([FromBody] GetStudentClasses data)
        {
            var query = from enrollements in _context.Enrollments
                        join cls in _context.Classes on enrollements.ClassId equals cls.ClassId
                        where enrollements.StudentId == data.StudentId
                        select new
                        {
                            class_id = cls.ClassId,
                            class_name = cls.ClassName
                        };
            var classData = query.ToList();

            if(classData == null || !classData.Any())
            {
                return NotFound("No classes found for the given student");
            }

            return Ok(classData);

        }


        [HttpPost("get-student-records")]
        public async Task<IActionResult> GetStudentRecords([FromBody] GetStudentRecords data)
        {
            var query = from classes in _context.Classes
                        join sessions in _context.Attendancesessions on classes.ClassId equals sessions.ClassId
                        join records in _context.Attendancerecords on sessions.SessionId equals records.SessionId
                        where records.StudentId == data.StudentId && classes.ClassId == data.ClassId
                        select new
                        {
                            session_id = records.SessionId,
                            status = records.Status
                        };

            var attendance_data = query.ToList();

            if(attendance_data == null || !attendance_data.Any())
            {
                return NotFound("No attendance records for this student");
            }
            return Ok(attendance_data);
        }

        [HttpPost("get-student-analytics")]
        public async Task<IActionResult> GetStudentAnalytics([FromBody] GetStudentAnalytics data)
        {
            var query = from classes in _context.Classes
                        join sessions in _context.Attendancesessions on classes.ClassId equals sessions.ClassId
                        join records in _context.Attendancerecords on sessions.SessionId equals records.SessionId
                        where records.StudentId == data.StudentId && classes.ClassId == data.ClassId
                        select new
                        {
                            session_id = records.SessionId,
                            status = records.Status
                        };
            var attendance_data = query.ToList();
            if (attendance_data == null || !attendance_data.Any())
            {
                return NotFound("No attendance records for this student");
            }
            var totalClasses = attendance_data.Count();
            var totalPresent = attendance_data.Count(x => x.status == "Present");
            var totalAbsent = attendance_data.Count(x => x.status == "Absent");
            var attendancePercentage = (double)totalPresent / totalClasses * 100;
            var analytics = new
            {
                total_classes = totalClasses,
                total_present = totalPresent,
                total_absent = totalAbsent,
                attendance_percentage = attendancePercentage
            };
            return Ok(analytics);
        }
    }
}
