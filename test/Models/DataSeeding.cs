using System;
using System.Collections.Generic;
using Bogus;
using Microsoft.EntityFrameworkCore;
namespace Models
{
    public static class DataSeeding
    {
        public static string lang = "fr";

       public static ModelBuilder Variants(this ModelBuilder modelBuilder)
                    {
                    int id = 1;
                    var faker = new Faker<Variant>(DataSeeding.lang)
                        .CustomInstantiator(f => new Variant { Id = id++ })
.RuleFor(o => o.Name, f => f.Lorem.Word())
.RuleFor(o => o.Description, f => f.Lorem.Word())
.RuleFor(o => o.AddedPrice, f => f.Random.Number(1, 10))
.RuleFor(o => o.Price, f => f.Random.Number(1, 10))
.RuleFor(o => o.Available, f => id - 1 == 1 ? true : f.Random.Bool())
.RuleFor(o => o.Stock, f => f.Random.Number(1, 10))
.RuleFor(o => o.DdOrder, f => f.Random.Number(1, 10))
.RuleFor(o => o.Items, f => f.Lorem.Word())
.RuleFor(o => o.AttributId, f => f.Random.Number(1, 10))
;
modelBuilder.Entity<Variant>().HasData(faker.Generate(10));
return modelBuilder;
}

public static ModelBuilder Roles(this ModelBuilder modelBuilder)
                    {
                    int id = 1;
                    var faker = new Faker<Role>(DataSeeding.lang)
                        .CustomInstantiator(f => new Role { Id = id++ })
.RuleFor(o => o.Value, f => f.Random.Number(1, 10))
;
modelBuilder.Entity<Role>().HasData(faker.Generate(10));
return modelBuilder;
}

public static ModelBuilder DiscountUsers(this ModelBuilder modelBuilder)
                    {
                    int id = 1;
                    var faker = new Faker<DiscountUser>(DataSeeding.lang)
                        .CustomInstantiator(f => new DiscountUser { Id = id++ })
.RuleFor(o => o.UserId, f => f.Random.Number(1, 10))
.RuleFor(o => o.DiscountId, f => f.Random.Number(1, 10))
;
modelBuilder.Entity<DiscountUser>().HasData(faker.Generate(10));
return modelBuilder;
}

public static ModelBuilder Users(this ModelBuilder modelBuilder)
                    {
                    int id = 1;
                    var faker = new Faker<User>(DataSeeding.lang)
                        .CustomInstantiator(f => new User { Id = id++ })
.RuleFor(o => o.Email, f => id - 1 == 1 ? "sa@angular.io" : f.Internet.Email())
.RuleFor(o => o.Password, f => "123")
.RuleFor(o => o.Username, f => f.Lorem.Word())
.RuleFor(o => o.Lastname, f => f.Lorem.Word())
.RuleFor(o => o.Firstname, f => f.Lorem.Word())
.RuleFor(o => o.Token, f => f.Lorem.Word())
.RuleFor(o => o.LanguageId, f => f.Random.Number(1, 10))
.RuleFor(o => o.RoleId, f => f.Random.Number(1, 10))
;
modelBuilder.Entity<User>().HasData(faker.Generate(10));
return modelBuilder;
}

public static ModelBuilder Units(this ModelBuilder modelBuilder)
                    {
                    int id = 1;
                    var faker = new Faker<Unit>(DataSeeding.lang)
                        .CustomInstantiator(f => new Unit { Id = id++ })
.RuleFor(o => o.Weight, f => f.Lorem.Word())
.RuleFor(o => o.Dimension, f => f.Lorem.Word())
.RuleFor(o => o.ShopId, f => f.Random.Number(1, 10))
;
modelBuilder.Entity<Unit>().HasData(faker.Generate(10));
return modelBuilder;
}

public static ModelBuilder Tickets(this ModelBuilder modelBuilder)
                    {
                    int id = 1;
                    var faker = new Faker<Ticket>(DataSeeding.lang)
                        .CustomInstantiator(f => new Ticket { Id = id++ })
.RuleFor(o => o.Subject, f => f.Lorem.Word())
.RuleFor(o => o.Message, f => f.Lorem.Word())
.RuleFor(o => o.Department, f => f.Lorem.Word())
.RuleFor(o => o.Priority, f => f.Lorem.Word())
.RuleFor(o => o.Status, f => f.Lorem.Word())
.RuleFor(o => o.Unread, f => id - 1 == 1 ? true : f.Random.Bool())
.RuleFor(o => o.ShopId, f => f.Lorem.Word())
;
modelBuilder.Entity<Ticket>().HasData(faker.Generate(10));
return modelBuilder;
}

public static ModelBuilder Stores(this ModelBuilder modelBuilder)
                    {
                    int id = 1;
                    var faker = new Faker<Store>(DataSeeding.lang)
                        .CustomInstantiator(f => new Store { Id = id++ })
.RuleFor(o => o.Name, f => f.Lorem.Word())
.RuleFor(o => o.Phone, f => f.Lorem.Word())
.RuleFor(o => o.Email, f => id - 1 == 1 ? "sa@angular.io" : f.Internet.Email())
.RuleFor(o => o.Address, f => f.Lorem.Word())
.RuleFor(o => o.Gps, f => f.Lorem.Word())
.RuleFor(o => o.ShopId, f => f.Random.Number(1, 10))
.RuleFor(o => o.CityId, f => f.Random.Number(1, 10))
;
modelBuilder.Entity<Store>().HasData(faker.Generate(10));
return modelBuilder;
}

public static ModelBuilder DeliveryMans(this ModelBuilder modelBuilder)
                    {
                    int id = 1;
                    var faker = new Faker<DeliveryMan>(DataSeeding.lang)
                        .CustomInstantiator(f => new DeliveryMan { Id = id++ })
.RuleFor(o => o.Name, f => f.Lorem.Word())
.RuleFor(o => o.Email, f => id - 1 == 1 ? "sa@angular.io" : f.Internet.Email())
.RuleFor(o => o.Tel, f => f.Lorem.Word())
;
modelBuilder.Entity<DeliveryMan>().HasData(faker.Generate(10));
return modelBuilder;
}

public static ModelBuilder Shops(this ModelBuilder modelBuilder)
                    {
                    int id = 1;
                    var faker = new Faker<Shop>(DataSeeding.lang)
                        .CustomInstantiator(f => new Shop { Id = id++ })
.RuleFor(o => o.CompanyName, f => f.Lorem.Word())
.RuleFor(o => o.SocialName, f => f.Lorem.Word())
.RuleFor(o => o.Description, f => f.Lorem.Word())
.RuleFor(o => o.Subdomain, f => f.Lorem.Word())
.RuleFor(o => o.Email, f => id - 1 == 1 ? "sa@angular.io" : f.Internet.Email())
.RuleFor(o => o.LogoUrl, f => f.Lorem.Word())
.RuleFor(o => o.HeaderUrl, f => f.Lorem.Word())
.RuleFor(o => o.ReimbursmentPolitic, f => f.Lorem.Word())
.RuleFor(o => o.AlwaysOpen, f => id - 1 == 1 ? true : f.Random.Bool())
.RuleFor(o => o.Preorder, f => id - 1 == 1 ? true : f.Random.Bool())
.RuleFor(o => o.Review, f => id - 1 == 1 ? true : f.Random.Bool())
.RuleFor(o => o.SatisfactionPolitic, f => f.Lorem.Word())
.RuleFor(o => o.Warranty, f => f.Lorem.Word())
.RuleFor(o => o.Active, f => id - 1 == 1 ? true : f.Random.Bool())
.RuleFor(o => o.List, f => id - 1 == 1 ? true : f.Random.Bool())
.RuleFor(o => o.Grid, f => id - 1 == 1 ? true : f.Random.Bool())
.RuleFor(o => o.TakeAway, f => id - 1 == 1 ? true : f.Random.Bool())
.RuleFor(o => o.ProductLimit, f => f.Random.Number(1, 10))
.RuleFor(o => o.PictureLimit, f => f.Random.Number(1, 10))
.RuleFor(o => o.CreationDate, f => f.Date.Past())
.RuleFor(o => o.EditDate, f => f.Date.Past())
.RuleFor(o => o.Phone, f => f.Lorem.Word())
.RuleFor(o => o.Facebook, f => f.Lorem.Word())
.RuleFor(o => o.Whatsapp, f => f.Lorem.Word())
.RuleFor(o => o.ActivityId, f => f.Random.Number(1, 10))
.RuleFor(o => o.DeliveryTypeId, f => f.Random.Number(1, 10))
.RuleFor(o => o.CashbackId, f => f.Random.Number(1, 10))
.RuleFor(o => o.DiscountId, f => f.Random.Number(1, 10))
;
modelBuilder.Entity<Shop>().HasData(faker.Generate(10));
return modelBuilder;
}

public static ModelBuilder ShopCustomerOrders(this ModelBuilder modelBuilder)
                    {
                    int id = 1;
                    var faker = new Faker<ShopCustomerOrder>(DataSeeding.lang)
                        .CustomInstantiator(f => new ShopCustomerOrder { Id = id++ })
.RuleFor(o => o.Date, f => f.Lorem.Word())
.RuleFor(o => o.CustomerId, f => f.Random.Number(1, 10))
.RuleFor(o => o.ShopId, f => f.Random.Number(1, 10))
.RuleFor(o => o.OrderId, f => f.Random.Number(1, 10))
;
modelBuilder.Entity<ShopCustomerOrder>().HasData(faker.Generate(10));
return modelBuilder;
}

public static ModelBuilder Reviews(this ModelBuilder modelBuilder)
                    {
                    int id = 1;
                    var faker = new Faker<Review>(DataSeeding.lang)
                        .CustomInstantiator(f => new Review { Id = id++ })
.RuleFor(o => o.Score, f => f.Random.Number(1, 10))
.RuleFor(o => o.Comment, f => f.Lorem.Word())
.RuleFor(o => o.CustomerId, f => f.Random.Number(1, 10))
.RuleFor(o => o.ProductId, f => f.Random.Number(1, 10))
;
modelBuilder.Entity<Review>().HasData(faker.Generate(10));
return modelBuilder;
}

public static ModelBuilder Regions(this ModelBuilder modelBuilder)
                    {
                    int id = 1;
                    var faker = new Faker<Region>(DataSeeding.lang)
                        .CustomInstantiator(f => new Region { Id = id++ })
.RuleFor(o => o.Name, f => f.Lorem.Word())
;
modelBuilder.Entity<Region>().HasData(faker.Generate(10));
return modelBuilder;
}

public static ModelBuilder Products(this ModelBuilder modelBuilder)
                    {
                    int id = 1;
                    var faker = new Faker<Product>(DataSeeding.lang)
                        .CustomInstantiator(f => new Product { Id = id++ })
.RuleFor(o => o.Name, f => f.Lorem.Word())
.RuleFor(o => o.Description, f => f.Lorem.Word())
.RuleFor(o => o.Price, f => f.Random.Number(1, 10))
.RuleFor(o => o.Available, f => id - 1 == 1 ? true : f.Random.Bool())
.RuleFor(o => o.Stock, f => f.Random.Number(1, 10))
.RuleFor(o => o.SuggestOnly, f => id - 1 == 1 ? true : f.Random.Bool())
.RuleFor(o => o.DdOrder, f => f.Random.Number(1, 10))
.RuleFor(o => o.ParentProductId, f => f.Random.Number(1, 10))
.RuleFor(o => o.FrontLine, f => id - 1 == 1 ? true : f.Random.Bool())
.RuleFor(o => o.Slug, f => f.Lorem.Word())
.RuleFor(o => o.MetaTitle, f => f.Lorem.Word())
.RuleFor(o => o.MetaDescription, f => f.Lorem.Word())
.RuleFor(o => o.SaleCount, f => f.Random.Number(1, 10))
.RuleFor(o => o.CreationDate, f => f.Date.Past())
.RuleFor(o => o.EditDate, f => f.Date.Past())
.RuleFor(o => o.Published, f => id - 1 == 1 ? true : f.Random.Bool())
.RuleFor(o => o.Publisher, f => f.Random.Number(1, 10))
.RuleFor(o => o.ExternalId, f => f.Random.Number(1, 10))
.RuleFor(o => o.DiscountPrice, f => f.Random.Number(1, 10))
.RuleFor(o => o.BrandId, f => f.Random.Number(1, 10))
.RuleFor(o => o.DiscountId, f => f.Random.Number(1, 10))
.RuleFor(o => o.ShopId, f => f.Random.Number(1, 10))
;
modelBuilder.Entity<Product>().HasData(faker.Generate(10));
return modelBuilder;
}

public static ModelBuilder ProductVariantCarts(this ModelBuilder modelBuilder)
                    {
                    int id = 1;
                    var faker = new Faker<ProductVariantCart>(DataSeeding.lang)
                        .CustomInstantiator(f => new ProductVariantCart { Id = id++ })
.RuleFor(o => o.LogoUrl, f => f.Lorem.Word())
.RuleFor(o => o.Quantity, f => f.Random.Number(1, 10))
.RuleFor(o => o.SelectedAttributItems, f => f.Lorem.Word())
.RuleFor(o => o.SelectedVariants, f => f.Lorem.Word())
.RuleFor(o => o.Discount, f => f.Lorem.Word())
.RuleFor(o => o.Product, f => f.Lorem.Word())
.RuleFor(o => o.CartId, f => f.Random.Number(1, 10))
;
modelBuilder.Entity<ProductVariantCart>().HasData(faker.Generate(10));
return modelBuilder;
}

public static ModelBuilder ProductAttributsVariants(this ModelBuilder modelBuilder)
                    {
                    int id = 1;
                    var faker = new Faker<ProductAttributsVariant>(DataSeeding.lang)
                        .CustomInstantiator(f => new ProductAttributsVariant { Id = id++ })
.RuleFor(o => o.Attributids, f => f.Lorem.Word())
.RuleFor(o => o.ProductId, f => f.Random.Number(1, 10))
.RuleFor(o => o.VariantId, f => f.Random.Number(1, 10))
;
modelBuilder.Entity<ProductAttributsVariant>().HasData(faker.Generate(10));
return modelBuilder;
}

public static ModelBuilder Pictures(this ModelBuilder modelBuilder)
                    {
                    int id = 1;
                    var faker = new Faker<Picture>(DataSeeding.lang)
                        .CustomInstantiator(f => new Picture { Id = id++ })
.RuleFor(o => o.Url, f => f.Lorem.Word())
.RuleFor(o => o.UrlTn, f => f.Lorem.Word())
.RuleFor(o => o.ProductId, f => f.Random.Number(1, 10))
;
modelBuilder.Entity<Picture>().HasData(faker.Generate(10));
return modelBuilder;
}

public static ModelBuilder PaymentStatuss(this ModelBuilder modelBuilder)
                    {
                    int id = 1;
                    var faker = new Faker<PaymentStatus>(DataSeeding.lang)
                        .CustomInstantiator(f => new PaymentStatus { Id = id++ })
.RuleFor(o => o.Value, f => f.Lorem.Word())
;
modelBuilder.Entity<PaymentStatus>().HasData(faker.Generate(10));
return modelBuilder;
}

public static ModelBuilder PaymentMethods(this ModelBuilder modelBuilder)
                    {
                    int id = 1;
                    var faker = new Faker<PaymentMethod>(DataSeeding.lang)
                        .CustomInstantiator(f => new PaymentMethod { Id = id++ })
.RuleFor(o => o.Type, f => f.Lorem.Word())
.RuleFor(o => o.Active, f => id - 1 == 1 ? true : f.Random.Bool())
;
modelBuilder.Entity<PaymentMethod>().HasData(faker.Generate(10));
return modelBuilder;
}

public static ModelBuilder Orders(this ModelBuilder modelBuilder)
                    {
                    int id = 1;
                    var faker = new Faker<Order>(DataSeeding.lang)
                        .CustomInstantiator(f => new Order { Id = id++ })
.RuleFor(o => o.OrderDate, f => f.Date.Past())
.RuleFor(o => o.DeliveryDate, f => f.Date.Past())
.RuleFor(o => o.Price, f => f.Random.Number(1, 10))
.RuleFor(o => o.DeliveryPrice, f => f.Random.Number(1, 10))
.RuleFor(o => o.Active, f => id - 1 == 1 ? true : f.Random.Bool())
.RuleFor(o => o.Number, f => f.Lorem.Word())
.RuleFor(o => o.EditDate, f => f.Date.Past())
.RuleFor(o => o.Cashback, f => f.Random.Number(1, 10))
.RuleFor(o => o.UsedCashback, f => f.Random.Number(1, 10))
.RuleFor(o => o.UsedDiscountCode, f => f.Lorem.Word())
.RuleFor(o => o.CartId, f => f.Random.Number(1, 10))
.RuleFor(o => o.AddressId, f => f.Random.Number(1, 10))
.RuleFor(o => o.DeliveryManId, f => f.Random.Number(1, 10))
.RuleFor(o => o.PaymentStatusId, f => f.Random.Number(1, 10))
.RuleFor(o => o.OrderStatusId, f => f.Random.Number(1, 10))
.RuleFor(o => o.PaymentMethodId, f => f.Random.Number(1, 10))
;
modelBuilder.Entity<Order>().HasData(faker.Generate(10));
return modelBuilder;
}

public static ModelBuilder OrderStatuss(this ModelBuilder modelBuilder)
                    {
                    int id = 1;
                    var faker = new Faker<OrderStatus>(DataSeeding.lang)
                        .CustomInstantiator(f => new OrderStatus { Id = id++ })
.RuleFor(o => o.Value, f => f.Lorem.Word())
.RuleFor(o => o.Sequence, f => f.Random.Number(1, 10))
;
modelBuilder.Entity<OrderStatus>().HasData(faker.Generate(10));
return modelBuilder;
}

public static ModelBuilder OpeningTimes(this ModelBuilder modelBuilder)
                    {
                    int id = 1;
                    var faker = new Faker<OpeningTime>(DataSeeding.lang)
                        .CustomInstantiator(f => new OpeningTime { Id = id++ })
.RuleFor(o => o.Day, f => f.Lorem.Word())
.RuleFor(o => o.StartTime, f => f.Date.Past())
.RuleFor(o => o.EndTime, f => f.Date.Past())
.RuleFor(o => o.ShopId, f => f.Random.Number(1, 10))
;
modelBuilder.Entity<OpeningTime>().HasData(faker.Generate(10));
return modelBuilder;
}

public static ModelBuilder Languages(this ModelBuilder modelBuilder)
                    {
                    int id = 1;
                    var faker = new Faker<Language>(DataSeeding.lang)
                        .CustomInstantiator(f => new Language { Id = id++ })
.RuleFor(o => o.Value, f => f.Lorem.Word())
;
modelBuilder.Entity<Language>().HasData(faker.Generate(10));
return modelBuilder;
}

public static ModelBuilder Discounts(this ModelBuilder modelBuilder)
                    {
                    int id = 1;
                    var faker = new Faker<Discount>(DataSeeding.lang)
                        .CustomInstantiator(f => new Discount { Id = id++ })
.RuleFor(o => o.Name, f => f.Lorem.Word())
.RuleFor(o => o.Description, f => f.Lorem.Word())
.RuleFor(o => o.Amount, f => f.Random.Number(1, 10))
.RuleFor(o => o.Percent, f => f.Random.Number(1, 10))
.RuleFor(o => o.StartDate, f => f.Date.Past())
.RuleFor(o => o.EndDate, f => f.Date.Past())
.RuleFor(o => o.Code, f => f.Lorem.Word())
.RuleFor(o => o.Affectations, f => f.Lorem.Word())
.RuleFor(o => o.AffectationTypeId, f => f.Random.Number(1, 10))
.RuleFor(o => o.DiscountTypeId, f => f.Random.Number(1, 10))
;
modelBuilder.Entity<Discount>().HasData(faker.Generate(10));
return modelBuilder;
}

public static ModelBuilder DiscountTypes(this ModelBuilder modelBuilder)
                    {
                    int id = 1;
                    var faker = new Faker<DiscountType>(DataSeeding.lang)
                        .CustomInstantiator(f => new DiscountType { Id = id++ })
.RuleFor(o => o.Value, f => f.Random.Number(1, 10))
;
modelBuilder.Entity<DiscountType>().HasData(faker.Generate(10));
return modelBuilder;
}

public static ModelBuilder Deliverys(this ModelBuilder modelBuilder)
                    {
                    int id = 1;
                    var faker = new Faker<Delivery>(DataSeeding.lang)
                        .CustomInstantiator(f => new Delivery { Id = id++ })
.RuleFor(o => o.Downtown, f => id - 1 == 1 ? true : f.Random.Bool())
.RuleFor(o => o.From, f => f.Random.Number(1, 10))
.RuleFor(o => o.To, f => f.Random.Number(1, 10))
.RuleFor(o => o.FreeFrom, f => f.Random.Number(1, 10))
.RuleFor(o => o.Radius, f => f.Random.Number(1, 10))
.RuleFor(o => o.MinOrderPrice, f => f.Random.Number(1, 10))
.RuleFor(o => o.Price, f => f.Random.Number(1, 10))
.RuleFor(o => o.DeliveryTime, f => f.Random.Number(1, 10))
.RuleFor(o => o.DeliveryModeId, f => f.Random.Number(1, 10))
.RuleFor(o => o.ShopId, f => f.Random.Number(1, 10))
;
modelBuilder.Entity<Delivery>().HasData(faker.Generate(10));
return modelBuilder;
}

public static ModelBuilder DeliveryTypes(this ModelBuilder modelBuilder)
                    {
                    int id = 1;
                    var faker = new Faker<DeliveryType>(DataSeeding.lang)
                        .CustomInstantiator(f => new DeliveryType { Id = id++ })
.RuleFor(o => o.Value, f => f.Random.Number(1, 10))
;
modelBuilder.Entity<DeliveryType>().HasData(faker.Generate(10));
return modelBuilder;
}

public static ModelBuilder DeliveryModes(this ModelBuilder modelBuilder)
                    {
                    int id = 1;
                    var faker = new Faker<DeliveryMode>(DataSeeding.lang)
                        .CustomInstantiator(f => new DeliveryMode { Id = id++ })
.RuleFor(o => o.Value, f => f.Random.Number(1, 10))
;
modelBuilder.Entity<DeliveryMode>().HasData(faker.Generate(10));
return modelBuilder;
}

public static ModelBuilder Customers(this ModelBuilder modelBuilder)
                    {
                    int id = 1;
                    var faker = new Faker<Customer>(DataSeeding.lang)
                        .CustomInstantiator(f => new Customer { Id = id++ })
.RuleFor(o => o.Firstname, f => f.Lorem.Word())
.RuleFor(o => o.Lastname, f => f.Lorem.Word())
.RuleFor(o => o.Email, f => id - 1 == 1 ? "sa@angular.io" : f.Internet.Email())
.RuleFor(o => o.Phone, f => f.Lorem.Word())
.RuleFor(o => o.SubscriptionDate, f => f.Date.Past())
.RuleFor(o => o.Cashback, f => f.Random.Number(1, 10))
;
modelBuilder.Entity<Customer>().HasData(faker.Generate(10));
return modelBuilder;
}

public static ModelBuilder Citys(this ModelBuilder modelBuilder)
                    {
                    int id = 1;
                    var faker = new Faker<City>(DataSeeding.lang)
                        .CustomInstantiator(f => new City { Id = id++ })
.RuleFor(o => o.Name, f => f.Lorem.Word())
.RuleFor(o => o.RegionId, f => f.Random.Number(1, 10))
;
modelBuilder.Entity<City>().HasData(faker.Generate(10));
return modelBuilder;
}

public static ModelBuilder Partners(this ModelBuilder modelBuilder)
                    {
                    int id = 1;
                    var faker = new Faker<Partner>(DataSeeding.lang)
                        .CustomInstantiator(f => new Partner { Id = id++ })
.RuleFor(o => o.Email, f => id - 1 == 1 ? "sa@angular.io" : f.Internet.Email())
;
modelBuilder.Entity<Partner>().HasData(faker.Generate(10));
return modelBuilder;
}

public static ModelBuilder Categorys(this ModelBuilder modelBuilder)
                    {
                    int id = 1;
                    var faker = new Faker<Category>(DataSeeding.lang)
                        .CustomInstantiator(f => new Category { Id = id++ })
.RuleFor(o => o.Name, f => f.Lorem.Word())
.RuleFor(o => o.Variant, f => id - 1 == 1 ? true : f.Random.Bool())
.RuleFor(o => o.Active, f => id - 1 == 1 ? true : f.Random.Bool())
.RuleFor(o => o.DdOrder, f => f.Random.Number(1, 10))
.RuleFor(o => o.Brand, f => id - 1 == 1 ? true : f.Random.Bool())
.RuleFor(o => o.DiscountId, f => f.Random.Number(1, 10))
.RuleFor(o => o.ShopId, f => f.Random.Number(1, 10))
.RuleFor(o => o.AdditionalFeeId, f => f.Random.Number(1, 10))
;
modelBuilder.Entity<Category>().HasData(faker.Generate(10));
return modelBuilder;
}

public static ModelBuilder Cashbacks(this ModelBuilder modelBuilder)
                    {
                    int id = 1;
                    var faker = new Faker<Cashback>(DataSeeding.lang)
                        .CustomInstantiator(f => new Cashback { Id = id++ })
.RuleFor(o => o.From, f => f.Random.Number(1, 10))
.RuleFor(o => o.ApplyFrom, f => f.Random.Number(1, 10))
.RuleFor(o => o.Percent, f => f.Random.Number(1, 10))
;
modelBuilder.Entity<Cashback>().HasData(faker.Generate(10));
return modelBuilder;
}

public static ModelBuilder Carts(this ModelBuilder modelBuilder)
                    {
                    int id = 1;
                    var faker = new Faker<Cart>(DataSeeding.lang)
                        .CustomInstantiator(f => new Cart { Id = id++ })
.RuleFor(o => o.Active, f => id - 1 == 1 ? true : f.Random.Bool())
.RuleFor(o => o.CreationDate, f => f.Lorem.Word())
;
modelBuilder.Entity<Cart>().HasData(faker.Generate(10));
return modelBuilder;
}

public static ModelBuilder Brands(this ModelBuilder modelBuilder)
                    {
                    int id = 1;
                    var faker = new Faker<Brand>(DataSeeding.lang)
                        .CustomInstantiator(f => new Brand { Id = id++ })
.RuleFor(o => o.Name, f => f.Lorem.Word())
;
modelBuilder.Entity<Brand>().HasData(faker.Generate(10));
return modelBuilder;
}

public static ModelBuilder Attributs(this ModelBuilder modelBuilder)
                    {
                    int id = 1;
                    var faker = new Faker<Attribut>(DataSeeding.lang)
                        .CustomInstantiator(f => new Attribut { Id = id++ })
.RuleFor(o => o.Name, f => f.Lorem.Word())
.RuleFor(o => o.Description, f => f.Lorem.Word())
.RuleFor(o => o.Min, f => f.Random.Number(1, 10))
.RuleFor(o => o.Max, f => f.Random.Number(1, 10))
.RuleFor(o => o.Extra, f => id - 1 == 1 ? true : f.Random.Bool())
.RuleFor(o => o.AttributTypeId, f => f.Random.Number(1, 10))
.RuleFor(o => o.CategoryId, f => f.Random.Number(1, 10))
;
modelBuilder.Entity<Attribut>().HasData(faker.Generate(10));
return modelBuilder;
}

public static ModelBuilder AttributTypes(this ModelBuilder modelBuilder)
                    {
                    int id = 1;
                    var faker = new Faker<AttributType>(DataSeeding.lang)
                        .CustomInstantiator(f => new AttributType { Id = id++ })
.RuleFor(o => o.Value, f => f.Lorem.Word())
;
modelBuilder.Entity<AttributType>().HasData(faker.Generate(10));
return modelBuilder;
}

public static ModelBuilder AttributItems(this ModelBuilder modelBuilder)
                    {
                    int id = 1;
                    var faker = new Faker<AttributItem>(DataSeeding.lang)
                        .CustomInstantiator(f => new AttributItem { Id = id++ })
.RuleFor(o => o.Name, f => f.Lorem.Word())
.RuleFor(o => o.AttributId, f => f.Random.Number(1, 10))
;
modelBuilder.Entity<AttributItem>().HasData(faker.Generate(10));
return modelBuilder;
}

public static ModelBuilder AffectationTypes(this ModelBuilder modelBuilder)
                    {
                    int id = 1;
                    var faker = new Faker<AffectationType>(DataSeeding.lang)
                        .CustomInstantiator(f => new AffectationType { Id = id++ })
.RuleFor(o => o.Value, f => f.Random.Number(1, 10))
;
modelBuilder.Entity<AffectationType>().HasData(faker.Generate(10));
return modelBuilder;
}

public static ModelBuilder _Addresss(this ModelBuilder modelBuilder)
                    {
                    int id = 1;
                    var faker = new Faker<_Address>(DataSeeding.lang)
                        .CustomInstantiator(f => new _Address { Id = id++ })
.RuleFor(o => o.Name, f => f.Lorem.Word())
.RuleFor(o => o.Gps, f => f.Lorem.Word())
.RuleFor(o => o.Value, f => f.Lorem.Word())
.RuleFor(o => o.Zipcode, f => f.Lorem.Word())
.RuleFor(o => o.CustomerId, f => f.Random.Number(1, 10))
.RuleFor(o => o.CityId, f => f.Random.Number(1, 10))
;
modelBuilder.Entity<_Address>().HasData(faker.Generate(10));
return modelBuilder;
}

public static ModelBuilder AdditionalFees(this ModelBuilder modelBuilder)
                    {
                    int id = 1;
                    var faker = new Faker<AdditionalFee>(DataSeeding.lang)
                        .CustomInstantiator(f => new AdditionalFee { Id = id++ })
.RuleFor(o => o.Price, f => f.Random.Number(1, 10))
.RuleFor(o => o.Name, f => f.Lorem.Word())
;
modelBuilder.Entity<AdditionalFee>().HasData(faker.Generate(10));
return modelBuilder;
}

public static ModelBuilder Activitys(this ModelBuilder modelBuilder)
                    {
                    int id = 1;
                    var faker = new Faker<Activity>(DataSeeding.lang)
                        .CustomInstantiator(f => new Activity { Id = id++ })
.RuleFor(o => o.Value, f => f.Lorem.Word())
;
modelBuilder.Entity<Activity>().HasData(faker.Generate(10));
return modelBuilder;
}

public static ModelBuilder _Actions(this ModelBuilder modelBuilder)
                    {
                    int id = 1;
                    var faker = new Faker<_Action>(DataSeeding.lang)
                        .CustomInstantiator(f => new _Action { Id = id++ })
.RuleFor(o => o.Date, f => f.Date.Past())
.RuleFor(o => o.OrderId, f => f.Random.Number(1, 10))
.RuleFor(o => o.OrderStatusId, f => f.Random.Number(1, 10))
;
modelBuilder.Entity<_Action>().HasData(faker.Generate(10));
return modelBuilder;
}

public static ModelBuilder Articles(this ModelBuilder modelBuilder)
                    {
                    int id = 1;
                    var faker = new Faker<Article>(DataSeeding.lang)
                        .CustomInstantiator(f => new Article { Id = id++ })
.RuleFor(o => o.Title, f => f.Lorem.Word())
.RuleFor(o => o.Content, f => f.Lorem.Word())
.RuleFor(o => o.Active, f => id - 1 == 1 ? true : f.Random.Bool())
.RuleFor(o => o.Slug, f => f.Lorem.Word())
.RuleFor(o => o.PageId, f => f.Random.Number(1, 10))
;
modelBuilder.Entity<Article>().HasData(faker.Generate(10));
return modelBuilder;
}

public static ModelBuilder Pages(this ModelBuilder modelBuilder)
                    {
                    int id = 1;
                    var faker = new Faker<Page>(DataSeeding.lang)
                        .CustomInstantiator(f => new Page { Id = id++ })
.RuleFor(o => o.Title, f => f.Lorem.Word())
.RuleFor(o => o.Description, f => f.Lorem.Word())
.RuleFor(o => o.Slug, f => f.Lorem.Word())
.RuleFor(o => o.Active, f => id - 1 == 1 ? true : f.Random.Bool())
;
modelBuilder.Entity<Page>().HasData(faker.Generate(10));
return modelBuilder;
}


    }
}