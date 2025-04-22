namespace AttendanceSystem.DTOs.Attendance
{
    public class MarkAttendance
    {
        public int ClassID { get; set; }
        public List<int> StudentIDs { get; set; }
        public DateTime Date { get; set; }
        public string Notes { get; set; }

    }
}
