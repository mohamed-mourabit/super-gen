using System;
using System.Text.Json.Serialization;
using System.Collections.Generic;
namespace Models
{
public partial class ProductVariantCart 
{public int Id { get; set; }
public string LogoUrl { get; set; }
public int Quantity { get; set; }
public string SelectedAttributItems { get; set; }
public string SelectedVariants { get; set; }
public string Discount { get; set; }
public string Product { get; set; }
public int CartId { get; set; }
[JsonIgnore]
public virtual Cart Cart { get; set; }
}
}
