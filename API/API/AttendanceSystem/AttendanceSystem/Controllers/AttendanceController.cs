using AttendanceSystem.DTOs.Attendance;
using AttendanceSystem.Models;
using AttendanceSystem.Utility;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceSystem.Controllers
{
    [ApiController]
    [JWTAuthorize]
    [Route("[controller]")]
    public class AttendanceController : Controller
    {
        private readonly AttendanceSystemContext _context;
        public AttendanceController(AttendanceSystemContext context)
        {
            _context = context;
        }

        [HttpPost("mark-attendance")]
        public async Task<IActionResult> MarkAttendance([FromBody] MarkAttendance data)
        {
            if (data == null)
            {
                return BadRequest("Invalid attendance data.");
            }
            if (data.ClassID == null || data.StudentIDs == null || data.Date == null)
            {
                return BadRequest("Class ID, Student IDs, and Date are required.");
            }
            var classExists = await _context.Classes.FindAsync(data.ClassID);
            if (classExists == null)
            {
                return NotFound("Class not found.");
            }
            var attendanceSession = await _context.Attendancesessions.AddAsync(new Attendancesession
            {
                ClassId = data.ClassID,
                SessionDate = data.Date,
                Notes = data.Notes
            });
            await _context.SaveChangesAsync();

            var enrolledStudents = _context.Enrollments
                                      .Where(e => e.ClassId == data.ClassID)
                                      .Select(e => e.StudentId)
                                      .ToList();
            var attendedStudents = data.StudentIDs;

            var attendanceRecords = new List<Attendancerecord>();

            foreach (var studentId in enrolledStudents)
            {
                var status = attendedStudents.Contains(studentId) ? "Present" : "Absent";
                attendanceRecords.Add(new Attendancerecord
                {
                    SessionId = attendanceSession.Entity.SessionId,
                    StudentId = studentId,
                    Status = status
                });
            }

            await _context.Attendancerecords.AddRangeAsync(attendanceRecords);

            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Attendance marked successfully.",
                sessionId = attendanceSession.Entity.SessionId
            });

        }

        [HttpPost("get-students")]
        public async Task<IActionResult> GetClassStudents([FromBody] GetClassStudentsDTO data)
        {
            if (data == null)
            {
                return BadRequest("Invalid Request!!");
            }
            if (data.classId == null)
            {
                return BadRequest("Class ID not provided!!");
            }
            var class_check = from classes in _context.Classes
                              where classes.ClassId == data.classId
                              select new
                              {
                                  class_id = classes.ClassId
                              };
            var class_check_data = class_check.ToList();
            if (class_check_data == null || !class_check_data.Any())
            {
                return BadRequest("Class Not Found!");
            }
            var query = from enrollments in _context.Enrollments
                               join users in _context.Users on enrollments.StudentId equals users.UserId
                               join masterusers in _context.Usermasters on users.UserId equals masterusers.MasterId
                               where enrollments.ClassId == data.classId
                               select new
                               {
                                   full_name = masterusers.FullName,
                                   student_id = enrollments.StudentId,
                               };
            var student_data = query.ToList();
            if(student_data == null || !student_data.Any())
            {
                return Ok("No Students Enrolled!");
            }
            return Ok(student_data);
        }
    }
}
