using System;
using System.Collections.Generic;

namespace AttendanceSystem.Models;

public partial class Class
{
    public int ClassId { get; set; }

    public string ClassName { get; set; } = null!;

    public int TeacherId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public virtual ICollection<Attendancesession> Attendancesessions { get; set; } = new List<Attendancesession>();

    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

    public virtual User Teacher { get; set; } = null!;
}
