using System;
using System.Text.Json.Serialization;
using System.Collections.Generic;
namespace Models
{
public partial class Cart 
{public int Id { get; set; }
public bool Active { get; set; }
public string CreationDate { get; set; }
[JsonIgnore]
public virtual ICollection<ProductVariantCart> ProductVariantCarts { get; set; }
[JsonIgnore]
public virtual ICollection<Order> Orders { get; set; }
}
}
