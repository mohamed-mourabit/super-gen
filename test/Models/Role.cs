using System;
using System.Text.Json.Serialization;
using System.Collections.Generic;
namespace Models
{
public partial class Role 
{public int Id { get; set; }
public int Value { get; set; }
[JsonIgnore]
public virtual ICollection<User> Users { get; set; }
}
}
