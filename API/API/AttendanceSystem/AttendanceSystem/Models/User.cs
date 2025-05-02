using System;
using System.Collections.Generic;

namespace AttendanceSystem.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Attendancerecord> Attendancerecords { get; set; } = new List<Attendancerecord>();

    public virtual ICollection<Class> Classes { get; set; } = new List<Class>();

    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

    public virtual Usermaster UserNavigation { get; set; } = null!;
}
