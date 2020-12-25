using System;
using System.Text.Json.Serialization;
using System.Collections.Generic;
namespace Models
{
public partial class Product 
{public int Id { get; set; }
public string Name { get; set; }
public string Description { get; set; }
public int Price { get; set; }
public bool Available { get; set; }
public int Stock { get; set; }
public bool SuggestOnly { get; set; }
public int DdOrder { get; set; }
public int ParentProductId { get; set; }
[JsonIgnore]
public virtual Product ParentProduct { get; set; }
[JsonIgnore]
public virtual ICollection<Product> ChildreProducts { get; set; }
public bool FrontLine { get; set; }
public string Slug { get; set; }
public string MetaTitle { get; set; }
public string MetaDescription { get; set; }
public int SaleCount { get; set; }
public DateTime CreationDate { get; set; }
public DateTime EditDate { get; set; }
public bool Published { get; set; }
public int Publisher { get; set; }
public int ExternalId { get; set; }
public int DiscountPrice { get; set; }
public int BrandId { get; set; }
[JsonIgnore]
public virtual Brand Brand { get; set; }
public int DiscountId { get; set; }
[JsonIgnore]
public virtual Discount Discount { get; set; }
public int ShopId { get; set; }
[JsonIgnore]
public virtual Shop Shop { get; set; }
[JsonIgnore]
public virtual ICollection<Picture> Pictures { get; set; }
[JsonIgnore]
public virtual ICollection<Attribut> Attributs { get; set; }
[JsonIgnore]
public virtual ICollection<Category> Categories { get; set; }
[JsonIgnore]
public virtual ICollection<ProductAttributsVariant> ProductAttributsVariants { get; set; }
[JsonIgnore]
public virtual ICollection<Customer> Customers { get; set; }
[JsonIgnore]
public virtual ICollection<Review> Reviews { get; set; }
}
}
