using System;
using System.Text.Json.Serialization;
using System.Collections.Generic;
namespace Models
{
public partial class DeliveryMan 
{public int Id { get; set; }
public string Name { get; set; }
public string Email { get; set; }
public string Tel { get; set; }
[JsonIgnore]
public virtual ICollection<Order> Orders { get; set; }
[JsonIgnore]
public virtual ICollection<Shop> Shops { get; set; }
}
}
