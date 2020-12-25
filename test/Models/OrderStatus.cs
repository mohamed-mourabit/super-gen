using System;
using System.Text.Json.Serialization;
using System.Collections.Generic;
namespace Models
{
public partial class OrderStatus 
{public int Id { get; set; }
public string Value { get; set; }
public int Sequence { get; set; }
[JsonIgnore]
public virtual ICollection<Order> Orders { get; set; }
[JsonIgnore]
public virtual ICollection<_Action> Actions { get; set; }
}
}
