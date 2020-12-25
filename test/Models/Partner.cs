using System;
using System.Text.Json.Serialization;
using System.Collections.Generic;
namespace Models
{
public partial class Partner 
{public int Id { get; set; }
public string Email { get; set; }
[JsonIgnore]
public virtual ICollection<Category> Categories { get; set; }
}
}
