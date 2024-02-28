using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BlogApplication.Models;

public partial class Blog
{
    public int Bid { get; set; }

    public int Uid { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    [JsonIgnore]
    public virtual ICollection<Comment>? Comments { get; set; } = new List<Comment>();
    [JsonIgnore]
    public virtual User? UidNavigation { get; set; }
}
