using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BlogApplication.Models;

public partial class Comment
{
    public int Cid { get; set; }

    public int? Uid { get; set; }

    public int? Bid { get; set; }

    public string? Comment1 { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    [JsonIgnore]
    public virtual Blog? BidNavigation { get; set; } 
  // [JsonIgnore]
    public virtual User? UidNavigation { get; set; } 
}
