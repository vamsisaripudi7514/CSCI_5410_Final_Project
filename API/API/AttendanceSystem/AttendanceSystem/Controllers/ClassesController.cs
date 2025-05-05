using AttendanceSystem.DTOs.Attendance;
using AttendanceSystem.DTOs.Classes;
using AttendanceSystem.Models;
using AttendanceSystem.Utility;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Drawing.Charts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System.Numerics;

namespace AttendanceSystem.Controllers
{
    [ApiController]
    [JWTAuthorize]
    [Route("[controller]")]
    public class ClassesController : Controller
    {
        private readonly AttendanceSystemContext _context;

        public ClassesController(AttendanceSystemContext context)
        {
            _context = context;
        }

        [HttpPost("create-class")]
        public async Task<IActionResult> CreateClass([FromForm] CreateClass data)
        {
            if (data == null )
            {
                return BadRequest("Payload Missing");
            }
            if(data.TeacherID == null)
            {
                return BadRequest("Teacher ID Missing!");
            }
            if (data.ClassName == null)
            {
                return BadRequest("Class Name missing!");
            }
            if(data.ExcelFile == null)
            {
                return BadRequest("Excel File Missing!");
            }

            bool classExists = _context.Classes.Any(c =>
                c.TeacherId == data.TeacherID && c.ClassName == data.ClassName);

           
            List<int> studentIds;
            try
            {
                studentIds = await GetStudentIDs(data.ExcelFile);
            }
            catch (Exception ex)
            {
                return BadRequest("Error reading Excel file: " + ex.Message);
            }

            var userMasterList =_context.Usermasters
                .Where(u => u.Role == "student")
                .ToList();

            var validIds = userMasterList
                .Where(u => studentIds.Contains(u.MasterId))
                .Select(u => u.MasterId)
                .ToList();

            var invalidIds = studentIds.Except(validIds).ToList();
            var count = 0;
            if (invalidIds.Any())
            {
                return BadRequest(new
                {
                    message = "Some Student IDs are invalid",
                    invalidStudentIds = invalidIds
                });
            }
            if (classExists)
            {
                var existingClass = _context.Classes.FirstOrDefault(c =>
                c.TeacherId == data.TeacherID && c.ClassName == data.ClassName);
                foreach (int studentId in validIds)
                {   
                    if(!_context.Enrollments.Any(c=>c.StudentId == studentId && c.ClassId == existingClass.ClassId)) { 
                    _context.Enrollments.Add(new Enrollment
                    {
                        ClassId = existingClass.ClassId,
                        StudentId = studentId,
                        EnrollmentDate = DateTime.Now
                    });count++;
                    }
                }
                await _context.SaveChangesAsync();
                return Ok(new
                {
                    message = $"Class '{existingClass.ClassId}' updated successfully.",
                    enrolledCount = count,
                    classId = existingClass.ClassId
                });

            }
            var newClass = new Class
            {
                ClassName = data.ClassName,
                TeacherId = data.TeacherID,
                CreatedDate = DateTime.Now
            };
            _context.Classes.Add(newClass);
            await _context.SaveChangesAsync(); 

            foreach (int studentId in validIds)
            {
                _context.Enrollments.Add(new Enrollment
                {
                    ClassId = newClass.ClassId,
                    StudentId = studentId,
                    EnrollmentDate = DateTime.Now
                });
            }

            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = $"Class '{data.ClassName}' created successfully.",
                enrolledCount = validIds.Count(),
                classId = newClass.ClassId
            });
        }


        //[HttpPost("delete-class")]
        //public async Task<IActionResult> DeleteClass([FromBody] DeleteClass data)
        //{
        //    if(data == null || data.ClassID == null || data.TeacherID == null)
        //    {
        //        return BadRequest("Payload or Payload Data Missing!");
        //    }
            
        //}
        [HttpPost("delete-class")]
        public async Task<IActionResult> DeleteClass([FromBody] DeleteClass data)
        {
            if (data == null || data.ClassID == null || data.TeacherID == null)
            {
                return BadRequest("Payload or Payload Data Missing!");
            }

            var classToDelete = await _context.Classes
                .Include(c => c.Enrollments)
                .Include(c => c.Attendancesessions)
                .ThenInclude(s => s.Attendancerecords)
                .FirstOrDefaultAsync(c => c.ClassId == data.ClassID && c.TeacherId == data.TeacherID);

            if (classToDelete == null)
            {
                return NotFound("Class not found or you do not have permission to delete this class.");
            }

            foreach (var session in classToDelete.Attendancesessions)
            {
                _context.Attendancerecords.RemoveRange(session.Attendancerecords);
            }

            _context.Attendancesessions.RemoveRange(classToDelete.Attendancesessions);

            _context.Enrollments.RemoveRange(classToDelete.Enrollments);

            _context.Classes.Remove(classToDelete);

            await _context.SaveChangesAsync();

            return Ok(new { message = "Class and related data deleted successfully." });
        }


        [HttpPost("parse-excel")]
        [Consumes("multipart/form-data")]
        public IActionResult ParseExcelClosedXML(IFormFile file)
        {
            var data = new List<int>();

            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            using (var stream = new MemoryStream())
            {
                file.CopyTo(stream);
                using (var workbook = new XLWorkbook(stream))
                {
                    var worksheet = workbook.Worksheet(1); // First sheet
                    var range = worksheet.RangeUsed();

                    int firstRow = range.FirstRowUsed().RowNumber();
                    int lastRow = range.LastRowUsed().RowNumber();
                    int firstCol = range.FirstColumnUsed().ColumnNumber();
                    int lastCol = range.LastColumnUsed().ColumnNumber();

                    for (int row = firstRow+1; row <= lastRow; row++)
                    {
                        for (int col = 1; col <= 1; col++)
                        {
                            var cellValue = worksheet.Cell(row, col).GetString();
                            if (!string.IsNullOrWhiteSpace(cellValue))
                                data.Add(int.Parse(cellValue));
                        }
                    }
                }
            }

            return Ok(data);
        }

        [HttpPost("getStudentId-file")]
        [Consumes("multipart/form-data")]
        public async Task<List<int>> GetStudentIDs(IFormFile file)
        {
            var data = new List<int>();

            if (file == null || file.Length == 0)
                return data;

            using (var stream = new MemoryStream())
            {
                file.CopyTo(stream);
                using (var workbook = new XLWorkbook(stream))
                {
                    var worksheet = workbook.Worksheet(1); // First sheet
                    var range = worksheet.RangeUsed();

                    int firstRow = range.FirstRowUsed().RowNumber();
                    int lastRow = range.LastRowUsed().RowNumber();
                    int firstCol = range.FirstColumnUsed().ColumnNumber();
                    int lastCol = range.LastColumnUsed().ColumnNumber();

                    for (int row = firstRow + 1; row <= lastRow; row++)
                    {
                        for (int col = 1; col <= 1; col++)
                        {
                            var cellValue = worksheet.Cell(row, col).GetString();
                            if (!string.IsNullOrWhiteSpace(cellValue))
                                data.Add(int.Parse(cellValue));
                        }
                    }
                }
            }
            return data;
        }

        [HttpPost("get-class-students")]
        public async Task<IActionResult> GetClassStudens([FromBody] GetClassStudentsDTO data)
        {
            if(data == null)
            {
                return BadRequest("Invalid payload!!");
            }
            if(data.classId == null)
            {
                return BadRequest("ClassID missing!");
            }
            var raw_data = from enrollments in _context.Enrollments
                           join masterusers in _context.Usermasters on enrollments.StudentId equals masterusers.MasterId
                           where enrollments.ClassId == data.classId
                           select new
                           {
                               studentID = masterusers.MasterId,
                               studentName = masterusers.FullName
                           };
            var student_data = raw_data.ToList();
            return Ok(student_data);
        }

        [HttpPost("drop-student")]
        public async Task<IActionResult> DeleteClassStudent([FromBody] DeleteClassStudentDTO data)
        {
            if (data == null || data.classID == null || data.studentID == null)
            {
                return BadRequest("Payload or Payload Data Missing!");
            }

            var studentToDrop = await _context.Enrollments
                                .FirstOrDefaultAsync(e => e.ClassId == data.classID && e.StudentId == data.studentID);
            if (studentToDrop == null)
            {
                return NotFound("Student not found, or you don't have the permission to perform the action!");
            }
            _context.Enrollments.Remove(studentToDrop);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Student dropped successfully." });
        }

    }
}

