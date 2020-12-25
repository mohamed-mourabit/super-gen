using Microsoft.EntityFrameworkCore;

namespace Models
{
    public partial class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options) { }

        public virtual DbSet<_Action> Actions { get; set; } 
public virtual DbSet<Activity> Activities { get; set; } 
public virtual DbSet<AdditionalFee> AdditionalFees { get; set; } 
public virtual DbSet<_Address> Addresses { get; set; } 
public virtual DbSet<AffectationType> AffectationTypes { get; set; } 
public virtual DbSet<AttributItem> AttributItems { get; set; } 
public virtual DbSet<AttributType> AttributTypes { get; set; } 
public virtual DbSet<Attribut> Attributs { get; set; } 
public virtual DbSet<Brand> Brands { get; set; } 
public virtual DbSet<Cart> Carts { get; set; } 
public virtual DbSet<Cashback> Cashbacks { get; set; } 
public virtual DbSet<Category> Categories { get; set; } 
public virtual DbSet<Partner> Partners { get; set; } 
public virtual DbSet<City> Cities { get; set; } 
public virtual DbSet<Customer> Customers { get; set; } 
public virtual DbSet<DeliveryMode> DeliveryModes { get; set; } 
public virtual DbSet<DeliveryType> DeliveryTypes { get; set; } 
public virtual DbSet<Delivery> Deliveries { get; set; } 
public virtual DbSet<DiscountType> DiscountTypes { get; set; } 
public virtual DbSet<Discount> Discounts { get; set; } 
public virtual DbSet<Language> Languages { get; set; } 
public virtual DbSet<OpeningTime> OpeningTimes { get; set; } 
public virtual DbSet<OrderStatus> OrderStatuses { get; set; } 
public virtual DbSet<Order> Orders { get; set; } 
public virtual DbSet<PaymentMethod> PaymentMethods { get; set; } 
public virtual DbSet<PaymentStatus> PaymentStatuses { get; set; } 
public virtual DbSet<Picture> Pictures { get; set; } 
public virtual DbSet<ProductAttributsVariant> ProductAttributsVariants { get; set; } 
public virtual DbSet<ProductVariantCart> ProductVariantCarts { get; set; } 
public virtual DbSet<Product> Products { get; set; } 
public virtual DbSet<Region> Regions { get; set; } 
public virtual DbSet<Review> Reviews { get; set; } 
public virtual DbSet<ShopCustomerOrder> ShopCustomerOrders { get; set; } 
public virtual DbSet<Shop> Shops { get; set; } 
public virtual DbSet<DeliveryMan> DeliveryMans { get; set; } 
public virtual DbSet<Store> Stores { get; set; } 
public virtual DbSet<Ticket> Tickets { get; set; } 
public virtual DbSet<Unit> Units { get; set; } 
public virtual DbSet<User> Users { get; set; } 
public virtual DbSet<Role> Roles { get; set; } 
public virtual DbSet<Variant> Variants { get; set; } 


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<_Action>(entity => 
{entity.HasKey(e => e.Id);
entity.Property(e => e.Id).ValueGeneratedOnAdd();
entity.Property(e => e.Date);
entity.Property(e => e.OrderId);
entity.HasOne(e => e.Order).WithMany(e => e.Actions).HasForeignKey(e => e.OrderId);
entity.Property(e => e.OrderStatusId);
entity.HasOne(e => e.OrderStatus).WithMany(e => e.Actions).HasForeignKey(e => e.OrderStatusId);
});

modelBuilder.Entity<Activity>(entity => 
{entity.HasKey(e => e.Id);
entity.Property(e => e.Id).ValueGeneratedOnAdd();
entity.Property(e => e.Value);
entity.HasMany(e => e.Shops).WithOne(p => p.Activity).HasForeignKey(e => e.ActivityId).OnDelete(DeleteBehavior.Cascade);
});

modelBuilder.Entity<AdditionalFee>(entity => 
{entity.HasKey(e => e.Id);
entity.Property(e => e.Id).ValueGeneratedOnAdd();
entity.Property(e => e.Price);
entity.Property(e => e.Name);
entity.HasMany(e => e.Categories).WithOne(p => p.AdditionalFee).HasForeignKey(e => e.AdditionalFeeId).OnDelete(DeleteBehavior.Cascade);
});

modelBuilder.Entity<_Address>(entity => 
{entity.HasKey(e => e.Id);
entity.Property(e => e.Id).ValueGeneratedOnAdd();
entity.Property(e => e.Name);
entity.Property(e => e.Gps);
entity.Property(e => e.Value);
entity.Property(e => e.Zipcode);
entity.Property(e => e.CustomerId);
entity.HasOne(e => e.Customer).WithMany(e => e.Addresses).HasForeignKey(e => e.CustomerId);
entity.Property(e => e.CityId);
entity.HasOne(e => e.City).WithMany(e => e.Addresses).HasForeignKey(e => e.CityId);
entity.HasMany(e => e.Orders).WithOne(p => p.Address).HasForeignKey(e => e.AddressId).OnDelete(DeleteBehavior.Cascade);
});

modelBuilder.Entity<AffectationType>(entity => 
{entity.HasKey(e => e.Id);
entity.Property(e => e.Id).ValueGeneratedOnAdd();
entity.Property(e => e.Value);
entity.HasMany(e => e.Discounts).WithOne(p => p.AffectationType).HasForeignKey(e => e.AffectationTypeId).OnDelete(DeleteBehavior.Cascade);
});

modelBuilder.Entity<AttributItem>(entity => 
{entity.HasKey(e => e.Id);
entity.Property(e => e.Id).ValueGeneratedOnAdd();
entity.Property(e => e.Name);
entity.Property(e => e.AttributId);
entity.HasOne(e => e.Attribut).WithMany(e => e.AttributItems).HasForeignKey(e => e.AttributId);
});

modelBuilder.Entity<AttributType>(entity => 
{entity.HasKey(e => e.Id);
entity.Property(e => e.Id).ValueGeneratedOnAdd();
entity.Property(e => e.Value);
entity.HasMany(e => e.Attributs).WithOne(p => p.AttributType).HasForeignKey(e => e.AttributTypeId).OnDelete(DeleteBehavior.Cascade);
});

modelBuilder.Entity<Attribut>(entity => 
{entity.HasKey(e => e.Id);
entity.Property(e => e.Id).ValueGeneratedOnAdd();
entity.Property(e => e.Name);
entity.Property(e => e.Description);
entity.Property(e => e.Min);
entity.Property(e => e.Max);
entity.Property(e => e.Extra);
entity.Property(e => e.AttributTypeId);
entity.HasOne(e => e.AttributType).WithMany(e => e.Attributs).HasForeignKey(e => e.AttributTypeId);
entity.Property(e => e.CategoryId);
entity.HasOne(e => e.Category).WithMany(e => e.Attributs).HasForeignKey(e => e.CategoryId);
entity.HasMany(e => e.Products).WithMany(p => p.Attributs);
entity.HasMany(e => e.Variants).WithOne(p => p.Attribut).HasForeignKey(e => e.AttributId).OnDelete(DeleteBehavior.Cascade);
entity.HasMany(e => e.AttributItems).WithOne(p => p.Attribut).HasForeignKey(e => e.AttributId).OnDelete(DeleteBehavior.Cascade);
});

modelBuilder.Entity<Brand>(entity => 
{entity.HasKey(e => e.Id);
entity.Property(e => e.Id).ValueGeneratedOnAdd();
entity.Property(e => e.Name);
entity.HasMany(e => e.Products).WithOne(p => p.Brand).HasForeignKey(e => e.BrandId).OnDelete(DeleteBehavior.Cascade);
});

modelBuilder.Entity<Cart>(entity => 
{entity.HasKey(e => e.Id);
entity.Property(e => e.Id).ValueGeneratedOnAdd();
entity.Property(e => e.Active);
entity.Property(e => e.CreationDate);
entity.HasMany(e => e.ProductVariantCarts).WithOne(p => p.Cart).HasForeignKey(e => e.CartId).OnDelete(DeleteBehavior.Cascade);
entity.HasMany(e => e.Orders).WithOne(p => p.Cart).HasForeignKey(e => e.CartId).OnDelete(DeleteBehavior.Cascade);
});

modelBuilder.Entity<Cashback>(entity => 
{entity.HasKey(e => e.Id);
entity.Property(e => e.Id).ValueGeneratedOnAdd();
entity.Property(e => e.From);
entity.Property(e => e.ApplyFrom);
entity.Property(e => e.Percent);
entity.HasMany(e => e.Shops).WithOne(p => p.Cashback).HasForeignKey(e => e.CashbackId).OnDelete(DeleteBehavior.Cascade);
});

modelBuilder.Entity<Category>(entity => 
{entity.HasKey(e => e.Id);
entity.Property(e => e.Id).ValueGeneratedOnAdd();
entity.Property(e => e.Name);
entity.Property(e => e.Variant);
entity.Property(e => e.Active);
entity.Property(e => e.DdOrder);
entity.Property(e => e.Brand);
entity.Property(e => e.DiscountId);
entity.HasOne(e => e.Discount).WithMany(e => e.Categories).HasForeignKey(e => e.DiscountId);
entity.Property(e => e.ShopId);
entity.HasOne(e => e.Shop).WithMany(e => e.Categories).HasForeignKey(e => e.ShopId);
entity.Property(e => e.AdditionalFeeId);
entity.HasOne(e => e.AdditionalFee).WithMany(e => e.Categories).HasForeignKey(e => e.AdditionalFeeId);
entity.HasMany(e => e.Products).WithMany(p => p.Categories);
entity.HasMany(e => e.Attributs).WithOne(p => p.Category).HasForeignKey(e => e.CategoryId).OnDelete(DeleteBehavior.Cascade);
entity.HasMany(e => e.Partners).WithMany(p => p.Categories);
});

modelBuilder.Entity<Partner>(entity => 
{entity.HasKey(e => e.Id);
entity.Property(e => e.Id).ValueGeneratedOnAdd();
entity.HasIndex(e => e.Email).IsUnique();
entity.HasMany(e => e.Categories).WithMany(p => p.Partners);
});

modelBuilder.Entity<City>(entity => 
{entity.HasKey(e => e.Id);
entity.Property(e => e.Id).ValueGeneratedOnAdd();
entity.Property(e => e.Name);
entity.Property(e => e.RegionId);
entity.HasOne(e => e.Region).WithMany(e => e.Cities).HasForeignKey(e => e.RegionId);
entity.HasMany(e => e.Deliveries).WithMany(p => p.Cities);
entity.HasMany(e => e.Stores).WithOne(p => p.City).HasForeignKey(e => e.CityId).OnDelete(DeleteBehavior.Cascade);
entity.HasMany(e => e.Addresses).WithOne(p => p.City).HasForeignKey(e => e.CityId).OnDelete(DeleteBehavior.Cascade);
});

modelBuilder.Entity<Customer>(entity => 
{entity.HasKey(e => e.Id);
entity.Property(e => e.Id).ValueGeneratedOnAdd();
entity.Property(e => e.Firstname);
entity.Property(e => e.Lastname);
entity.HasIndex(e => e.Email).IsUnique();
entity.Property(e => e.Phone);
entity.Property(e => e.SubscriptionDate);
entity.Property(e => e.Cashback);
entity.HasMany(e => e.Addresses).WithOne(p => p.Customer).HasForeignKey(e => e.CustomerId).OnDelete(DeleteBehavior.Cascade);
entity.HasMany(e => e.ShopCustomerOrders).WithOne(p => p.Customer).HasForeignKey(e => e.CustomerId).OnDelete(DeleteBehavior.Cascade);
entity.HasMany(e => e.Products).WithMany(p => p.Customers);
entity.HasMany(e => e.Reviews).WithOne(p => p.Customer).HasForeignKey(e => e.CustomerId).OnDelete(DeleteBehavior.Cascade);
});

modelBuilder.Entity<DeliveryMode>(entity => 
{entity.HasKey(e => e.Id);
entity.Property(e => e.Id).ValueGeneratedOnAdd();
entity.Property(e => e.Value);
entity.HasMany(e => e.Deliveries).WithOne(p => p.DeliveryMode).HasForeignKey(e => e.DeliveryModeId).OnDelete(DeleteBehavior.Cascade);
});

modelBuilder.Entity<DeliveryType>(entity => 
{entity.HasKey(e => e.Id);
entity.Property(e => e.Id).ValueGeneratedOnAdd();
entity.Property(e => e.Value);
entity.HasMany(e => e.Shops).WithOne(p => p.DeliveryType).HasForeignKey(e => e.DeliveryTypeId).OnDelete(DeleteBehavior.Cascade);
});

modelBuilder.Entity<Delivery>(entity => 
{entity.HasKey(e => e.Id);
entity.Property(e => e.Id).ValueGeneratedOnAdd();
entity.Property(e => e.Downtown);
entity.Property(e => e.From);
entity.Property(e => e.To);
entity.Property(e => e.FreeFrom);
entity.Property(e => e.Radius);
entity.Property(e => e.MinOrderPrice);
entity.Property(e => e.Price);
entity.Property(e => e.DeliveryTime);
entity.Property(e => e.DeliveryModeId);
entity.HasOne(e => e.DeliveryMode).WithMany(e => e.Deliveries).HasForeignKey(e => e.DeliveryModeId);
entity.Property(e => e.ShopId);
entity.HasOne(e => e.Shop).WithMany(e => e.Deliveries).HasForeignKey(e => e.ShopId);
entity.HasMany(e => e.Cities).WithMany(p => p.Deliveries);
});

modelBuilder.Entity<DiscountType>(entity => 
{entity.HasKey(e => e.Id);
entity.Property(e => e.Id).ValueGeneratedOnAdd();
entity.Property(e => e.Value);
entity.HasMany(e => e.Discounts).WithOne(p => p.DiscountType).HasForeignKey(e => e.DiscountTypeId).OnDelete(DeleteBehavior.Cascade);
});

modelBuilder.Entity<Discount>(entity => 
{entity.HasKey(e => e.Id);
entity.Property(e => e.Id).ValueGeneratedOnAdd();
entity.Property(e => e.Name);
entity.Property(e => e.Description);
entity.Property(e => e.Amount);
entity.Property(e => e.Percent);
entity.Property(e => e.StartDate);
entity.Property(e => e.EndDate);
entity.Property(e => e.Code);
entity.Property(e => e.Affectations);
entity.HasMany(e => e.Shops).WithOne(p => p.Discount).HasForeignKey(e => e.DiscountId).OnDelete(DeleteBehavior.Cascade);
entity.Property(e => e.AffectationTypeId);
entity.HasOne(e => e.AffectationType).WithMany(e => e.Discounts).HasForeignKey(e => e.AffectationTypeId);
entity.Property(e => e.DiscountTypeId);
entity.HasOne(e => e.DiscountType).WithMany(e => e.Discounts).HasForeignKey(e => e.DiscountTypeId);
entity.HasMany(e => e.Products).WithOne(p => p.Discount).HasForeignKey(e => e.DiscountId).OnDelete(DeleteBehavior.Cascade);
entity.HasMany(e => e.Categories).WithOne(p => p.Discount).HasForeignKey(e => e.DiscountId).OnDelete(DeleteBehavior.Cascade);
});

modelBuilder.Entity<Language>(entity => 
{entity.HasKey(e => e.Id);
entity.Property(e => e.Id).ValueGeneratedOnAdd();
entity.Property(e => e.Value);
entity.HasMany(e => e.UserCredentials).WithOne(p => p.Language).HasForeignKey(e => e.LanguageId).OnDelete(DeleteBehavior.Cascade);
});

modelBuilder.Entity<OpeningTime>(entity => 
{entity.HasKey(e => e.Id);
entity.Property(e => e.Id).ValueGeneratedOnAdd();
entity.Property(e => e.Day);
entity.Property(e => e.StartTime);
entity.Property(e => e.EndTime);
entity.Property(e => e.ShopId);
entity.HasOne(e => e.Shop).WithMany(e => e.OpeningTimes).HasForeignKey(e => e.ShopId);
});

modelBuilder.Entity<OrderStatus>(entity => 
{entity.HasKey(e => e.Id);
entity.Property(e => e.Id).ValueGeneratedOnAdd();
entity.Property(e => e.Value);
entity.Property(e => e.Sequence);
entity.HasMany(e => e.Orders).WithOne(p => p.OrderStatus).HasForeignKey(e => e.OrderStatusId).OnDelete(DeleteBehavior.Cascade);
entity.HasMany(e => e.Actions).WithOne(p => p.OrderStatus).HasForeignKey(e => e.OrderStatusId).OnDelete(DeleteBehavior.Cascade);
});

modelBuilder.Entity<Order>(entity => 
{entity.HasKey(e => e.Id);
entity.Property(e => e.Id).ValueGeneratedOnAdd();
entity.Property(e => e.OrderDate);
entity.Property(e => e.DeliveryDate);
entity.Property(e => e.Price);
entity.Property(e => e.DeliveryPrice);
entity.Property(e => e.Active);
entity.Property(e => e.Number);
entity.Property(e => e.EditDate);
entity.Property(e => e.Cashback);
entity.Property(e => e.UsedCashback);
entity.Property(e => e.UsedDiscountCode);
entity.Property(e => e.CartId);
entity.HasOne(e => e.Cart).WithMany(e => e.Orders).HasForeignKey(e => e.CartId);
entity.Property(e => e.AddressId);
entity.HasOne(e => e.Address).WithMany(e => e.Orders).HasForeignKey(e => e.AddressId);
entity.Property(e => e.DeliveryManId);
entity.HasOne(e => e.DeliveryMan).WithMany(e => e.Orders).HasForeignKey(e => e.DeliveryManId);
entity.Property(e => e.PaymentStatusId);
entity.HasOne(e => e.PaymentStatus).WithMany(e => e.Orders).HasForeignKey(e => e.PaymentStatusId);
entity.Property(e => e.OrderStatusId);
entity.HasOne(e => e.OrderStatus).WithMany(e => e.Orders).HasForeignKey(e => e.OrderStatusId);
entity.Property(e => e.PaymentMethodId);
entity.HasOne(e => e.PaymentMethod).WithMany(e => e.Orders).HasForeignKey(e => e.PaymentMethodId);
entity.HasMany(e => e.Actions).WithOne(p => p.Order).HasForeignKey(e => e.OrderId).OnDelete(DeleteBehavior.Cascade);
entity.HasMany(e => e.ShopCustomerOrders).WithOne(p => p.Order).HasForeignKey(e => e.OrderId).OnDelete(DeleteBehavior.Cascade);
});

modelBuilder.Entity<PaymentMethod>(entity => 
{entity.HasKey(e => e.Id);
entity.Property(e => e.Id).ValueGeneratedOnAdd();
entity.Property(e => e.Type);
entity.Property(e => e.Active);
entity.HasMany(e => e.Shops).WithMany(p => p.PaymentMethods);
entity.HasMany(e => e.Orders).WithOne(p => p.PaymentMethod).HasForeignKey(e => e.PaymentMethodId).OnDelete(DeleteBehavior.Cascade);
});

modelBuilder.Entity<PaymentStatus>(entity => 
{entity.HasKey(e => e.Id);
entity.Property(e => e.Id).ValueGeneratedOnAdd();
entity.Property(e => e.Value);
entity.HasMany(e => e.Orders).WithOne(p => p.PaymentStatus).HasForeignKey(e => e.PaymentStatusId).OnDelete(DeleteBehavior.Cascade);
});

modelBuilder.Entity<Picture>(entity => 
{entity.HasKey(e => e.Id);
entity.Property(e => e.Id).ValueGeneratedOnAdd();
entity.Property(e => e.Url);
entity.Property(e => e.UrlTn);
entity.Property(e => e.ProductId);
entity.HasOne(e => e.Product).WithMany(e => e.Pictures).HasForeignKey(e => e.ProductId);
});

modelBuilder.Entity<ProductAttributsVariant>(entity => 
{entity.HasKey(e => e.Id);
entity.Property(e => e.Id).ValueGeneratedOnAdd();
entity.Property(e => e.Attributids);
entity.Property(e => e.ProductId);
entity.Property(e => e.VariantId);
entity.HasOne(e => e.Variant).WithMany(e => e.ProductAttributsVariants).HasForeignKey(e => e.VariantId);
entity.HasOne(e => e.Product).WithMany(e => e.ProductAttributsVariants).HasForeignKey(e => e.ProductId);
});

modelBuilder.Entity<ProductVariantCart>(entity => 
{entity.HasKey(e => e.Id);
entity.Property(e => e.Id).ValueGeneratedOnAdd();
entity.Property(e => e.LogoUrl);
entity.Property(e => e.Quantity);
entity.Property(e => e.SelectedAttributItems);
entity.Property(e => e.SelectedVariants);
entity.Property(e => e.Discount);
entity.Property(e => e.Product);
entity.Property(e => e.CartId);
entity.HasOne(e => e.Cart).WithMany(e => e.ProductVariantCarts).HasForeignKey(e => e.CartId);
});

modelBuilder.Entity<Product>(entity => 
{entity.HasKey(e => e.Id);
entity.Property(e => e.Id).ValueGeneratedOnAdd();
entity.Property(e => e.Name);
entity.Property(e => e.Description);
entity.Property(e => e.Price);
entity.Property(e => e.Available);
entity.Property(e => e.Stock);
entity.Property(e => e.SuggestOnly);
entity.Property(e => e.DdOrder);
entity.Property(e => e.ParentProductId);
entity.HasOne(e => e.ParentProduct).WithMany(e => e.Products).HasForeignKey(e => e.ParentProductId);
entity.HasMany(e => e.ChildreProducts).WithOne(p => p.Product).HasForeignKey(e => e.ProductId).OnDelete(DeleteBehavior.Cascade);
entity.Property(e => e.FrontLine);
entity.Property(e => e.Slug);
entity.Property(e => e.MetaTitle);
entity.Property(e => e.MetaDescription);
entity.Property(e => e.SaleCount);
entity.Property(e => e.CreationDate);
entity.Property(e => e.EditDate);
entity.Property(e => e.Published);
entity.Property(e => e.Publisher);
entity.Property(e => e.ExternalId);
entity.Property(e => e.DiscountPrice);
entity.Property(e => e.BrandId);
entity.HasOne(e => e.Brand).WithMany(e => e.Products).HasForeignKey(e => e.BrandId);
entity.Property(e => e.DiscountId);
entity.HasOne(e => e.Discount).WithMany(e => e.Products).HasForeignKey(e => e.DiscountId);
entity.Property(e => e.ShopId);
entity.HasOne(e => e.Shop).WithMany(e => e.Products).HasForeignKey(e => e.ShopId);
entity.HasMany(e => e.Pictures).WithOne(p => p.Product).HasForeignKey(e => e.ProductId).OnDelete(DeleteBehavior.Cascade);
entity.HasMany(e => e.Attributs).WithMany(p => p.Products);
entity.HasMany(e => e.Categories).WithMany(p => p.Products);
entity.HasMany(e => e.ProductAttributsVariants).WithOne(p => p.Product).HasForeignKey(e => e.ProductId).OnDelete(DeleteBehavior.Cascade);
entity.HasMany(e => e.Customers).WithMany(p => p.Products);
entity.HasMany(e => e.Reviews).WithOne(p => p.Product).HasForeignKey(e => e.ProductId).OnDelete(DeleteBehavior.Cascade);
});

modelBuilder.Entity<Region>(entity => 
{entity.HasKey(e => e.Id);
entity.Property(e => e.Id).ValueGeneratedOnAdd();
entity.Property(e => e.Name);
entity.HasMany(e => e.Cities).WithOne(p => p.Region).HasForeignKey(e => e.RegionId).OnDelete(DeleteBehavior.Cascade);
});

modelBuilder.Entity<Review>(entity => 
{entity.HasKey(e => e.Id);
entity.Property(e => e.Id).ValueGeneratedOnAdd();
entity.Property(e => e.Score);
entity.Property(e => e.Comment);
entity.Property(e => e.CustomerId);
entity.HasOne(e => e.Customer).WithMany(e => e.Reviews).HasForeignKey(e => e.CustomerId);
entity.Property(e => e.ProductId);
entity.HasOne(e => e.Product).WithMany(e => e.Reviews).HasForeignKey(e => e.ProductId);
});

modelBuilder.Entity<ShopCustomerOrder>(entity => 
{entity.HasKey(e => e.Id);
entity.Property(e => e.Id).ValueGeneratedOnAdd();
entity.Property(e => e.Date);
entity.Property(e => e.CustomerId);
entity.HasOne(e => e.Customer).WithMany(e => e.ShopCustomerOrders).HasForeignKey(e => e.CustomerId);
entity.Property(e => e.ShopId);
entity.HasOne(e => e.Shop).WithMany(e => e.ShopCustomerOrders).HasForeignKey(e => e.ShopId);
entity.Property(e => e.OrderId);
entity.HasOne(e => e.Order).WithMany(e => e.ShopCustomerOrders).HasForeignKey(e => e.OrderId);
});

modelBuilder.Entity<Shop>(entity => 
{entity.HasKey(e => e.Id);
entity.Property(e => e.Id).ValueGeneratedOnAdd();
entity.Property(e => e.CompanyName);
entity.Property(e => e.SocialName);
entity.Property(e => e.Description);
entity.Property(e => e.Subdomain);
entity.HasIndex(e => e.Email).IsUnique();
entity.Property(e => e.LogoUrl);
entity.Property(e => e.HeaderUrl);
entity.Property(e => e.ReimbursmentPolitic);
entity.Property(e => e.AlwaysOpen);
entity.Property(e => e.Preorder);
entity.Property(e => e.Review);
entity.Property(e => e.SatisfactionPolitic);
entity.Property(e => e.Warranty);
entity.Property(e => e.Active);
entity.Property(e => e.List);
entity.Property(e => e.Grid);
entity.Property(e => e.TakeAway);
entity.Property(e => e.ProductLimit);
entity.Property(e => e.PictureLimit);
entity.Property(e => e.CreationDate);
entity.Property(e => e.EditDate);
entity.Property(e => e.Phone);
entity.Property(e => e.Facebook);
entity.Property(e => e.Whatsapp);
entity.Property(e => e.ActivityId);
entity.HasOne(e => e.Activity).WithMany(e => e.Shops).HasForeignKey(e => e.ActivityId);
entity.Property(e => e.DeliveryTypeId);
entity.HasOne(e => e.DeliveryType).WithMany(e => e.Shops).HasForeignKey(e => e.DeliveryTypeId);
entity.Property(e => e.CashbackId);
entity.HasOne(e => e.Cashback).WithMany(e => e.Shops).HasForeignKey(e => e.CashbackId);
entity.HasMany(e => e.Units).WithOne(p => p.Shop).HasForeignKey(e => e.ShopId).OnDelete(DeleteBehavior.Cascade);
entity.Property(e => e.DiscountId);
entity.HasOne(e => e.Discount).WithMany(e => e.Shops).HasForeignKey(e => e.DiscountId);
entity.HasMany(e => e.OpeningTimes).WithOne(p => p.Shop).HasForeignKey(e => e.ShopId).OnDelete(DeleteBehavior.Cascade);
entity.HasMany(e => e.Categories).WithOne(p => p.Shop).HasForeignKey(e => e.ShopId).OnDelete(DeleteBehavior.Cascade);
entity.HasMany(e => e.Products).WithOne(p => p.Shop).HasForeignKey(e => e.ShopId).OnDelete(DeleteBehavior.Cascade);
entity.HasMany(e => e.Stores).WithOne(p => p.Shop).HasForeignKey(e => e.ShopId).OnDelete(DeleteBehavior.Cascade);
entity.HasMany(e => e.DeliveryMans).WithMany(p => p.Shops);
entity.HasMany(e => e.Deliveries).WithOne(p => p.Shop).HasForeignKey(e => e.ShopId).OnDelete(DeleteBehavior.Cascade);
entity.HasMany(e => e.PaymentMethods).WithMany(p => p.Shops);
entity.HasMany(e => e.ShopCustomerOrders).WithOne(p => p.Shop).HasForeignKey(e => e.ShopId).OnDelete(DeleteBehavior.Cascade);
});

modelBuilder.Entity<DeliveryMan>(entity => 
{entity.HasKey(e => e.Id);
entity.Property(e => e.Id).ValueGeneratedOnAdd();
entity.Property(e => e.Name);
entity.HasIndex(e => e.Email).IsUnique();
entity.Property(e => e.Tel);
entity.HasMany(e => e.Orders).WithOne(p => p.DeliveryMan).HasForeignKey(e => e.DeliveryManId).OnDelete(DeleteBehavior.Cascade);
entity.HasMany(e => e.Shops).WithMany(p => p.DeliveryMans);
});

modelBuilder.Entity<Store>(entity => 
{entity.HasKey(e => e.Id);
entity.Property(e => e.Id).ValueGeneratedOnAdd();
entity.Property(e => e.Name);
entity.Property(e => e.Phone);
entity.HasIndex(e => e.Email).IsUnique();
entity.Property(e => e.Address);
entity.Property(e => e.Gps);
entity.Property(e => e.ShopId);
entity.HasOne(e => e.Shop).WithMany(e => e.Stores).HasForeignKey(e => e.ShopId);
entity.Property(e => e.CityId);
entity.HasOne(e => e.City).WithMany(e => e.Stores).HasForeignKey(e => e.CityId);
});

modelBuilder.Entity<Ticket>(entity => 
{entity.HasKey(e => e.Id);
entity.Property(e => e.Id).ValueGeneratedOnAdd();
entity.Property(e => e.Subject);
entity.Property(e => e.Message);
entity.Property(e => e.Department);
entity.Property(e => e.Priority);
entity.Property(e => e.Status);
entity.Property(e => e.Unread);
entity.Property(e => e.ShopId);
});

modelBuilder.Entity<Unit>(entity => 
{entity.HasKey(e => e.Id);
entity.Property(e => e.Id).ValueGeneratedOnAdd();
entity.Property(e => e.Weight);
entity.Property(e => e.Dimension);
entity.Property(e => e.ShopId);
entity.HasOne(e => e.Shop).WithMany(e => e.Units).HasForeignKey(e => e.ShopId);
});

modelBuilder.Entity<User>(entity => 
{entity.HasKey(e => e.Id);
entity.Property(e => e.Id).ValueGeneratedOnAdd();
entity.HasIndex(e => e.Email).IsUnique();
entity.Property(e => e.Password);
entity.Property(e => e.Username);
entity.Property(e => e.Lastname);
entity.Property(e => e.Firstname);
entity.Property(e => e.Token);
entity.Property(e => e.LanguageId);
entity.HasOne(e => e.Language).WithMany(e => e.Users).HasForeignKey(e => e.LanguageId);
entity.Property(e => e.RoleId);
entity.HasOne(e => e.Role).WithMany(e => e.Users).HasForeignKey(e => e.RoleId);
});

modelBuilder.Entity<Role>(entity => 
{entity.HasKey(e => e.Id);
entity.Property(e => e.Id).ValueGeneratedOnAdd();
entity.Property(e => e.Value);
entity.HasMany(e => e.Users).WithOne(p => p.Role).HasForeignKey(e => e.RoleId).OnDelete(DeleteBehavior.Cascade);
});

modelBuilder.Entity<Variant>(entity => 
{entity.HasKey(e => e.Id);
entity.Property(e => e.Id).ValueGeneratedOnAdd();
entity.Property(e => e.Name);
entity.Property(e => e.Description);
entity.Property(e => e.AddedPrice);
entity.Property(e => e.Price);
entity.Property(e => e.Available);
entity.Property(e => e.Stock);
entity.Property(e => e.DdOrder);
entity.Property(e => e.Items);
entity.Property(e => e.AttributId);
entity.HasOne(e => e.Attribut).WithMany(e => e.Variants).HasForeignKey(e => e.AttributId);
entity.HasMany(e => e.ProductAttributsVariants).WithOne(p => p.Variant).HasForeignKey(e => e.VariantId).OnDelete(DeleteBehavior.Cascade);
});




            modelBuilder
                .Variants()
.Roles()
.Users()
.Units()
.Tickets()
.Stores()
.DeliveryMans()
.Shops()
.ShopCustomerOrders()
.Reviews()
.Regions()
.Products()
.ProductVariantCarts()
.ProductAttributsVariants()
.Pictures()
.PaymentStatuss()
.PaymentMethods()
.Orders()
.OrderStatuss()
.OpeningTimes()
.Languages()
.Discounts()
.DiscountTypes()
.Deliverys()
.DeliveryTypes()
.DeliveryModes()
.Customers()
.Citys()
.Partners()
.Categorys()
.Cashbacks()
.Carts()
.Brands()
.Attributs()
.AttributTypes()
.AttributItems()
.AffectationTypes()
._Addresss()
.AdditionalFees()
.Activitys()
._Actions()

                ;
        }


        // partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
