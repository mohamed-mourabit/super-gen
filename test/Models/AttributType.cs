using System;
using System.Text.Json.Serialization;
using System.Collections.Generic;
namespace Models
{
public partial class AttributType 
{public int Id { get; set; }
public string Value { get; set; }
[JsonIgnore]
public virtual ICollection<Attribut> Attributs { get; set; }
}
}
