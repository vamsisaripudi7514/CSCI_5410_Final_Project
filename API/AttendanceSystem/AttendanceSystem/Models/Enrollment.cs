using System;
using System.Collections.Generic;

namespace AttendanceSystem.Models;

public partial class Enrollment
{
    public int EnrollmentId { get; set; }

    public int ClassId { get; set; }

    public int StudentId { get; set; }

    public DateTime? EnrollmentDate { get; set; }

    public virtual Class Class { get; set; } = null!;

    public virtual User Student { get; set; } = null!;
}
