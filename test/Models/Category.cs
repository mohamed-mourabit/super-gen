using System;
using System.Text.Json.Serialization;
using System.Collections.Generic;
namespace Models
{
public partial class Category 
{public int Id { get; set; }
public string Name { get; set; }
public bool Variant { get; set; }
public bool Active { get; set; }
public int DdOrder { get; set; }
public bool Brand { get; set; }
public int DiscountId { get; set; }
[JsonIgnore]
public virtual Discount Discount { get; set; }
public int ShopId { get; set; }
[JsonIgnore]
public virtual Shop Shop { get; set; }
public int AdditionalFeeId { get; set; }
[JsonIgnore]
public virtual AdditionalFee AdditionalFee { get; set; }
[JsonIgnore]
public virtual ICollection<Product> Products { get; set; }
[JsonIgnore]
public virtual ICollection<Attribut> Attributs { get; set; }
[JsonIgnore]
public virtual ICollection<Partner> Partners { get; set; }
}
}
