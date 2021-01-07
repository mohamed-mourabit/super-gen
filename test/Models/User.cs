using System;
using System.Text.Json.Serialization;
using System.Collections.Generic;
namespace Models
{
public partial class User 
{public int Id { get; set; }
public string Email { get; set; }
public string Password { get; set; }
public string Username { get; set; }
public string Lastname { get; set; }
public string Firstname { get; set; }
public string Token { get; set; }
public int LanguageId { get; set; }
[JsonIgnore]
public virtual Language Language { get; set; }
public int RoleId { get; set; }
[JsonIgnore]
public virtual Role Role { get; set; }
[JsonIgnore]
public virtual ICollection<DiscountUser> DiscountUsers { get; set; }
}
}
