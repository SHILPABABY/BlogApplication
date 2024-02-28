using System;
using System.Collections.Generic;

namespace BlogApplication.Models;

public partial class Registration
{
    public int Rid { get; set; }

    public string? Uname { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public int? IsActive { get; set; }
}
