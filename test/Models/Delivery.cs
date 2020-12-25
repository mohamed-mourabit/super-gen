using System;
using System.Text.Json.Serialization;
using System.Collections.Generic;
namespace Models
{
public partial class Delivery 
{public int Id { get; set; }
public bool Downtown { get; set; }
public int From { get; set; }
public int To { get; set; }
public int FreeFrom { get; set; }
public int Radius { get; set; }
public int MinOrderPrice { get; set; }
public int Price { get; set; }
public int DeliveryTime { get; set; }
public int DeliveryModeId { get; set; }
[JsonIgnore]
public virtual DeliveryMode DeliveryMode { get; set; }
public int ShopId { get; set; }
[JsonIgnore]
public virtual Shop Shop { get; set; }
[JsonIgnore]
public virtual ICollection<City> Cities { get; set; }
}
}
