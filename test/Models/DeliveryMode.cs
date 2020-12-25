using System;
using System.Text.Json.Serialization;
using System.Collections.Generic;
namespace Models
{
public partial class DeliveryMode 
{public int Id { get; set; }
public int Value { get; set; }
[JsonIgnore]
public virtual ICollection<Delivery> Deliveries { get; set; }
}
}
