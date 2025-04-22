using AttendanceSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace AttendanceSystem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReportController : Controller
    {
        private readonly AttendanceSystemContext _context;
        public ReportController(AttendanceSystemContext context)
        {
            _context = context;
        }

        [HttpPost("get-class-report")]
        public async Task<IActionResult> GetClassReport([FromBody] GetClassReport data)
        {
            if (data == null || data.classID == null)
            {
                return BadRequest("Payload or Payload Data Missing!");
            }
            var classInfo = await _context.Classes
               .FirstOrDefaultAsync(c => c.ClassId == data.classID);

            if (classInfo == null)
                return NotFound("Class not found.");

            int totalSessions = await _context.Attendancesessions
                .CountAsync(s => s.ClassId == data.classID);

            var enrolledStudents = await _context.Enrollments
                .Where(e => e.ClassId == data.classID)
                .Include(e => e.Student)
                .Select(e => new
                {
                    e.Student.UserId,
                    e.Student.Username,
                })
                .ToListAsync();
            QuestPDF.Settings.License = LicenseType.Community;
            var attendanceCounts = await _context.Attendancerecords
                .Where(r => r.Session.ClassId == data.classID && r.Status == "Present")
                .GroupBy(r => r.StudentId)
                .Select(g => new
                {
                    StudentId = g.Key,
                    Count = g.Count()
                })
                .ToListAsync();

            var pdf = Document.Create(container =>
            {

                container.Page(page =>
                {
                    page.Margin(30);
                    page.Header().PaddingBottom(10).Text($"Attendance Report: {classInfo.ClassName}")
                        .FontSize(20).Bold().AlignCenter();

                    page.Content().Table(table =>
                    {

                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn(2); 
                            columns.RelativeColumn(3); 
                            columns.RelativeColumn(2); 
                            columns.RelativeColumn(2); 
                        });

                        table.Header(header =>
                        {
                            header.Cell().Element(CellStyle).ExtendHorizontal().Text("Student ID").Bold();
                            header.Cell().Element(CellStyle).ExtendHorizontal().Text("Name").Bold();
                            header.Cell().Element(CellStyle).ExtendHorizontal().Text("Attendance Count").Bold();
                            header.Cell().Element(CellStyle).ExtendHorizontal().Text("Percentage").Bold();
                        });

                        foreach (var student in enrolledStudents)
                        {
                            int attended = attendanceCounts
                                .FirstOrDefault(a => a.StudentId == student.UserId)?.Count ?? 0;

                            double percentage = totalSessions > 0
                                ? (attended / (double)totalSessions) * 100
                                : 0;

                            table.Cell().Element(CellStyle).Text(student.UserId.ToString());
                            table.Cell().Element(CellStyle).Text(student.Username);
                            table.Cell().Element(CellStyle).Text($"{attended} / {totalSessions}");
                            table.Cell().Element(CellStyle).Text($"{percentage:F2}%");
                        }
                    });

                    page.Footer().AlignCenter().Text(txt =>
                    {
                        txt.Span("Generated on ");
                        txt.Span(DateTime.Now.ToString("yyyy-MM-dd HH:mm")).SemiBold();
                    });
                });
            }).GeneratePdf();

            return File(pdf, "application/pdf", $"AttendanceReport_{classInfo.ClassName}.pdf");
        }
        private static IContainer CellStyle(IContainer container)
        {
            return container
                .Border(1)
                .BorderColor(QuestPDF.Helpers.Colors.Grey.Medium)
                .Padding(5)
                .AlignLeft();
        }
    }
}
