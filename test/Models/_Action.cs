using System;
using System.Text.Json.Serialization;
using System.Collections.Generic;
namespace Models
{
public partial class _Action 
{public int Id { get; set; }
public DateTime Date { get; set; }
public int OrderId { get; set; }
[JsonIgnore]
public virtual Order Order { get; set; }
public int OrderStatusId { get; set; }
[JsonIgnore]
public virtual OrderStatus OrderStatus { get; set; }
}
}
