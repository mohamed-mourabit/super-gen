using System;
using System.Text.Json.Serialization;
using System.Collections.Generic;
namespace Models
{
public partial class Shop 
{public int Id { get; set; }
public string CompanyName { get; set; }
public string SocialName { get; set; }
public string Description { get; set; }
public string Subdomain { get; set; }
public string Email { get; set; }
public string LogoUrl { get; set; }
public string HeaderUrl { get; set; }
public string ReimbursmentPolitic { get; set; }
public bool AlwaysOpen { get; set; }
public bool Preorder { get; set; }
public bool Review { get; set; }
public string SatisfactionPolitic { get; set; }
public string Warranty { get; set; }
public bool Active { get; set; }
public bool List { get; set; }
public bool Grid { get; set; }
public bool TakeAway { get; set; }
public int ProductLimit { get; set; }
public int PictureLimit { get; set; }
public DateTime CreationDate { get; set; }
public DateTime EditDate { get; set; }
public string Phone { get; set; }
public string Facebook { get; set; }
public string Whatsapp { get; set; }
public int ActivityId { get; set; }
[JsonIgnore]
public virtual Activity Activity { get; set; }
public int DeliveryTypeId { get; set; }
[JsonIgnore]
public virtual DeliveryType DeliveryType { get; set; }
public int CashbackId { get; set; }
[JsonIgnore]
public virtual Cashback Cashback { get; set; }
[JsonIgnore]
public virtual ICollection<Unit> Units { get; set; }
public int DiscountId { get; set; }
[JsonIgnore]
public virtual Discount Discount { get; set; }
[JsonIgnore]
public virtual ICollection<OpeningTime> OpeningTimes { get; set; }
[JsonIgnore]
public virtual ICollection<Category> Categories { get; set; }
[JsonIgnore]
public virtual ICollection<Product> Products { get; set; }
[JsonIgnore]
public virtual ICollection<Store> Stores { get; set; }
[JsonIgnore]
public virtual ICollection<DeliveryMan> DeliveryMans { get; set; }
[JsonIgnore]
public virtual ICollection<Delivery> Deliveries { get; set; }
[JsonIgnore]
public virtual ICollection<PaymentMethod> PaymentMethods { get; set; }
[JsonIgnore]
public virtual ICollection<ShopCustomerOrder> ShopCustomerOrders { get; set; }
}
}
