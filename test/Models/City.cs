using System;
using System.Text.Json.Serialization;
using System.Collections.Generic;
namespace Models
{
public partial class City 
{public int Id { get; set; }
public string Name { get; set; }
public int RegionId { get; set; }
[JsonIgnore]
public virtual Region Region { get; set; }
[JsonIgnore]
public virtual ICollection<Delivery> Deliveries { get; set; }
[JsonIgnore]
public virtual ICollection<Store> Stores { get; set; }
[JsonIgnore]
public virtual ICollection<_Address> Addresses { get; set; }
}
}
