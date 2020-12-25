using System;
using System.Text.Json.Serialization;
using System.Collections.Generic;
namespace Models
{
public partial class Store 
{public int Id { get; set; }
public string Name { get; set; }
public string Phone { get; set; }
public string Email { get; set; }
public string Address { get; set; }
public string Gps { get; set; }
public int ShopId { get; set; }
[JsonIgnore]
public virtual Shop Shop { get; set; }
public int CityId { get; set; }
[JsonIgnore]
public virtual City City { get; set; }
}
}
