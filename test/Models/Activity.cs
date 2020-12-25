using System;
using System.Text.Json.Serialization;
using System.Collections.Generic;
namespace Models
{
public partial class Activity 
{public int Id { get; set; }
public string Value { get; set; }
[JsonIgnore]
public virtual ICollection<Shop> Shops { get; set; }
}
}
