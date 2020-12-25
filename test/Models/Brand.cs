using System;
using System.Text.Json.Serialization;
using System.Collections.Generic;
namespace Models
{
public partial class Brand 
{public int Id { get; set; }
public string Name { get; set; }
[JsonIgnore]
public virtual ICollection<Product> Products { get; set; }
}
}
