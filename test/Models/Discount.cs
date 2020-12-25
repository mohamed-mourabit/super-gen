using System;
using System.Text.Json.Serialization;
using System.Collections.Generic;
namespace Models
{
public partial class Discount 
{public int Id { get; set; }
public string Name { get; set; }
public string Description { get; set; }
public int Amount { get; set; }
public int Percent { get; set; }
public DateTime StartDate { get; set; }
public DateTime EndDate { get; set; }
public string Code { get; set; }
public string Affectations { get; set; }
[JsonIgnore]
public virtual ICollection<Shop> Shops { get; set; }
public int AffectationTypeId { get; set; }
[JsonIgnore]
public virtual AffectationType AffectationType { get; set; }
public int DiscountTypeId { get; set; }
[JsonIgnore]
public virtual DiscountType DiscountType { get; set; }
[JsonIgnore]
public virtual ICollection<Product> Products { get; set; }
[JsonIgnore]
public virtual ICollection<Category> Categories { get; set; }
}
}
