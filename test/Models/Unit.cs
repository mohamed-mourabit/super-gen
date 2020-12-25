using System;
using System.Text.Json.Serialization;
using System.Collections.Generic;
namespace Models
{
public partial class Unit 
{public int Id { get; set; }
public string Weight { get; set; }
public string Dimension { get; set; }
public int ShopId { get; set; }
[JsonIgnore]
public virtual Shop Shop { get; set; }
}
}
