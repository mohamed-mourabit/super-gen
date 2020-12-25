using System;
using System.Text.Json.Serialization;
using System.Collections.Generic;
namespace Models
{
public partial class _Address 
{public int Id { get; set; }
public string Name { get; set; }
public string Gps { get; set; }
public string Value { get; set; }
public string Zipcode { get; set; }
public int CustomerId { get; set; }
[JsonIgnore]
public virtual Customer Customer { get; set; }
public int CityId { get; set; }
[JsonIgnore]
public virtual City City { get; set; }
[JsonIgnore]
public virtual ICollection<Order> Orders { get; set; }
}
}
