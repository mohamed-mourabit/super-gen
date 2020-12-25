using System;
using System.Text.Json.Serialization;
using System.Collections.Generic;
namespace Models
{
public partial class DiscountType 
{public int Id { get; set; }
public int Value { get; set; }
[JsonIgnore]
public virtual ICollection<Discount> Discounts { get; set; }
}
}
