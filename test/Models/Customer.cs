using System;
using System.Text.Json.Serialization;
using System.Collections.Generic;
namespace Models
{
public partial class Customer 
{public int Id { get; set; }
public string Firstname { get; set; }
public string Lastname { get; set; }
public string Email { get; set; }
public string Phone { get; set; }
public DateTime SubscriptionDate { get; set; }
public int Cashback { get; set; }
[JsonIgnore]
public virtual ICollection<_Address> Addresses { get; set; }
[JsonIgnore]
public virtual ICollection<ShopCustomerOrder> ShopCustomerOrders { get; set; }
[JsonIgnore]
public virtual ICollection<Product> Products { get; set; }
[JsonIgnore]
public virtual ICollection<Review> Reviews { get; set; }
}
}
