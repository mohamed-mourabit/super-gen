using System;
using System.Text.Json.Serialization;
using System.Collections.Generic;
namespace Models
{
public partial class AttributItem 
{public int Id { get; set; }
public string Name { get; set; }
public int AttributId { get; set; }
[JsonIgnore]
public virtual Attribut Attribut { get; set; }
}
}
