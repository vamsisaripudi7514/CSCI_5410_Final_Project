namespace AttendanceSystem.Models
{
    public class CreateClass
    {
        public int TeacherID { get; set; }
        public string ClassName { get; set; }
        public IFormFile ExcelFile { get; set; }
    }
}
