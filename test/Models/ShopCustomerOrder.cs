using System;
using System.Text.Json.Serialization;
using System.Collections.Generic;
namespace Models
{
public partial class ShopCustomerOrder 
{public int Id { get; set; }
public string Date { get; set; }
public int CustomerId { get; set; }
[JsonIgnore]
public virtual Customer Customer { get; set; }
public int ShopId { get; set; }
[JsonIgnore]
public virtual Shop Shop { get; set; }
public int OrderId { get; set; }
[JsonIgnore]
public virtual Order Order { get; set; }
}
}
