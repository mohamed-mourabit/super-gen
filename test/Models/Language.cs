using System;
using System.Text.Json.Serialization;
using System.Collections.Generic;
namespace Models
{
public partial class Language 
{public int Id { get; set; }
public string Value { get; set; }
[JsonIgnore]
public virtual ICollection<User> UserCredentials { get; set; }
}
}
