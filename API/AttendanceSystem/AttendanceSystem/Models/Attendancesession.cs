using System;
using System.Collections.Generic;

namespace AttendanceSystem.Models;

public partial class Attendancesession
{
    public int SessionId { get; set; }

    public int ClassId { get; set; }

    public DateTime SessionDate { get; set; }

    public string? Notes { get; set; }

    public virtual ICollection<Attendancerecord> Attendancerecords { get; set; } = new List<Attendancerecord>();

    public virtual Class Class { get; set; } = null!;
}
