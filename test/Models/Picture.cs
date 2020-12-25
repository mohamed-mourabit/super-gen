using System;
using System.Text.Json.Serialization;
using System.Collections.Generic;
namespace Models
{
public partial class Picture 
{public int Id { get; set; }
public string Url { get; set; }
public string UrlTn { get; set; }
public int ProductId { get; set; }
[JsonIgnore]
public virtual Product Product { get; set; }
}
}
