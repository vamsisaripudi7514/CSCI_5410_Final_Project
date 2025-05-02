using System;
using System.Collections.Generic;

namespace AttendanceSystem.Models;

public partial class Usermaster
{
    public int MasterId { get; set; }

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Role { get; set; } = null!;

    public virtual User? User { get; set; }
}
