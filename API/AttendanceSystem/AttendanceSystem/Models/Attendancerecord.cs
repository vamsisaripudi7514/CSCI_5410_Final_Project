using System;
using System.Collections.Generic;

namespace AttendanceSystem.Models;

public partial class Attendancerecord
{
    public int RecordId { get; set; }

    public int SessionId { get; set; }

    public int StudentId { get; set; }

    public string Status { get; set; } = null!;

    public virtual Attendancesession Session { get; set; } = null!;

    public virtual User Student { get; set; } = null!;
}
