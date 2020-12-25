using System;
using System.Text.Json.Serialization;
using System.Collections.Generic;
namespace Models
{
public partial class OpeningTime 
{public int Id { get; set; }
public string Day { get; set; }
public DateTime StartTime { get; set; }
public DateTime EndTime { get; set; }
public int ShopId { get; set; }
[JsonIgnore]
public virtual Shop Shop { get; set; }
}
}
