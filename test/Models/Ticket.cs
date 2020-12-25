using System;
using System.Text.Json.Serialization;
using System.Collections.Generic;
namespace Models
{
public partial class Ticket 
{public int Id { get; set; }
public string Subject { get; set; }
public string Message { get; set; }
public string Department { get; set; }
public string Priority { get; set; }
public string Status { get; set; }
public bool Unread { get; set; }
public string ShopId { get; set; }
}
}
