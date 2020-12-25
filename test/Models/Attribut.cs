using System;
using System.Text.Json.Serialization;
using System.Collections.Generic;
namespace Models
{
public partial class Attribut 
{public int Id { get; set; }
public string Name { get; set; }
public string Description { get; set; }
public int Min { get; set; }
public int Max { get; set; }
public bool Extra { get; set; }
public int AttributTypeId { get; set; }
[JsonIgnore]
public virtual AttributType AttributType { get; set; }
public int CategoryId { get; set; }
[JsonIgnore]
public virtual Category Category { get; set; }
[JsonIgnore]
public virtual ICollection<Product> Products { get; set; }
[JsonIgnore]
public virtual ICollection<Variant> Variants { get; set; }
[JsonIgnore]
public virtual ICollection<AttributItem> AttributItems { get; set; }
}
}
