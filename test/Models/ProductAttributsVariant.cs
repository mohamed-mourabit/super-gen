using System;
using System.Text.Json.Serialization;
using System.Collections.Generic;
namespace Models
{
public partial class ProductAttributsVariant 
{public int Id { get; set; }
public string Attributids { get; set; }
public int ProductId { get; set; }
public int VariantId { get; set; }
[JsonIgnore]
public virtual Variant Variant { get; set; }
[JsonIgnore]
public virtual Product Product { get; set; }
}
}
