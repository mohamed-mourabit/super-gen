using System;
using System.Text.Json.Serialization;
using System.Collections.Generic;
namespace Models
{
public partial class AdditionalFee 
{public int Id { get; set; }
public int Price { get; set; }
public string Name { get; set; }
[JsonIgnore]
public virtual ICollection<Category> Categories { get; set; }
}
}
