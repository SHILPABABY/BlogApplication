using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BlogApplication.Models;

public partial class User
{
    public int Id { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public bool? IsActive { get; set; }
    [JsonIgnore]

    public virtual ICollection<Blog> Blogs { get; set; } = new List<Blog>();
    [JsonIgnore]
    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
}
