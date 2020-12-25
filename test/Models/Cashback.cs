using System;
using System.Text.Json.Serialization;
using System.Collections.Generic;
namespace Models
{
public partial class Cashback 
{public int Id { get; set; }
public int From { get; set; }
public int ApplyFrom { get; set; }
public int Percent { get; set; }
[JsonIgnore]
public virtual ICollection<Shop> Shops { get; set; }
}
}
