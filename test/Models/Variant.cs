using System;
using System.Text.Json.Serialization;
using System.Collections.Generic;
namespace Models
{
public partial class Variant 
{public int Id { get; set; }
public string Name { get; set; }
public string Description { get; set; }
public int AddedPrice { get; set; }
public int Price { get; set; }
public bool Available { get; set; }
public int Stock { get; set; }
public int DdOrder { get; set; }
public string Items { get; set; }
public int AttributId { get; set; }
[JsonIgnore]
public virtual Attribut Attribut { get; set; }
[JsonIgnore]
public virtual ICollection<ProductAttributsVariant> ProductAttributsVariants { get; set; }
}
}
