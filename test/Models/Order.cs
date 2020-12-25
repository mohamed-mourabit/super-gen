using System;
using System.Text.Json.Serialization;
using System.Collections.Generic;
namespace Models
{
public partial class Order 
{public int Id { get; set; }
public DateTime OrderDate { get; set; }
public DateTime DeliveryDate { get; set; }
public int Price { get; set; }
public int DeliveryPrice { get; set; }
public bool Active { get; set; }
public string Number { get; set; }
public DateTime EditDate { get; set; }
public int Cashback { get; set; }
public int UsedCashback { get; set; }
public string UsedDiscountCode { get; set; }
public int CartId { get; set; }
[JsonIgnore]
public virtual Cart Cart { get; set; }
public int AddressId { get; set; }
[JsonIgnore]
public virtual _Address Address { get; set; }
public int DeliveryManId { get; set; }
[JsonIgnore]
public virtual DeliveryMan DeliveryMan { get; set; }
public int PaymentStatusId { get; set; }
[JsonIgnore]
public virtual PaymentStatus PaymentStatus { get; set; }
public int OrderStatusId { get; set; }
[JsonIgnore]
public virtual OrderStatus OrderStatus { get; set; }
public int PaymentMethodId { get; set; }
[JsonIgnore]
public virtual PaymentMethod PaymentMethod { get; set; }
[JsonIgnore]
public virtual ICollection<_Action> Actions { get; set; }
[JsonIgnore]
public virtual ICollection<ShopCustomerOrder> ShopCustomerOrders { get; set; }
}
}
