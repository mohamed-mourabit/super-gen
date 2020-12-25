using System;
using System.Text.Json.Serialization;
using System.Collections.Generic;
namespace Models
{
public partial class Review 
{public int Id { get; set; }
public int Score { get; set; }
public string Comment { get; set; }
public int CustomerId { get; set; }
[JsonIgnore]
public virtual Customer Customer { get; set; }
public int ProductId { get; set; }
[JsonIgnore]
public virtual Product Product { get; set; }
}
}
