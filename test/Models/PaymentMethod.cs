using System;
using System.Text.Json.Serialization;
using System.Collections.Generic;
namespace Models
{
public partial class PaymentMethod 
{public int Id { get; set; }
public string Type { get; set; }
public bool Active { get; set; }
[JsonIgnore]
public virtual ICollection<Shop> Shops { get; set; }
[JsonIgnore]
public virtual ICollection<Order> Orders { get; set; }
}
}
