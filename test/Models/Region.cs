using System;
using System.Text.Json.Serialization;
using System.Collections.Generic;
namespace Models
{
public partial class Region 
{public int Id { get; set; }
public string Name { get; set; }
[JsonIgnore]
public virtual ICollection<City> Cities { get; set; }
}
}
