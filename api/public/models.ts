export class Action {
  id: number;
  date: Date;
  orderId: number;
  order: Order; //Migration
  orderStatusId: number;
  orderStatus: OrderStatus; //Migration
}

export class Activity {
  id: number;
  value: string;
  shops: Shop[]; //Migration
}


export class AdditionalFee {
  id: number;
  price: number;
  name: string;
  categories: Category[]; //Migration
}

export class _Address {
  id: number;
  name: string;
  gps: string;
  value: string;
  zipcode: string;
  customerId: number;
  customer: Customer; //Migration
  cityId: number;
  city: City;

  orders: Order[]; //Migration
}

export class AffetationType {
  id: number;
  value: number;
  discounts: Array<Discount>; //Migration
}

export class AttributItem {
  id: number;
  name: string;
  attributId: number;
  attribut: Attribut; //Migration
}

export class AttributType {
  id: number;
  value: string;
  attributs: Array<Attribut>;  //Migration
}

export class Attribut {
  id: number;
  name: string;
  description: string;
  min: number;
  max: number;
  extra: boolean;

  attributTypeId: number;
  attributType: AttributType;

  categoryId: number;
  category: Category;

  products: Array<Product>; //Migration
  variants: Array<Variant>; //Extra=true
  attributItems: AttributItem[];
}

export class Brand {
  id: number;
  name: string;

  products: Product[];//Migration
}

export class Cart {
  id: number;
  active: boolean;
  creationDate: string;

  productVariantCarts: Array<ProductVariantCart>;

  orders: Order[];//Migration
}

export class Cashback {
  id: number;
  from: number;
  applyFrom: number;
  percent: number;

  shops: Shop[];//Migration
}

export class Category {
  id: number;
  name: string;
  variant: boolean;
  active: boolean;
  ddOrder: number;
  brand: boolean;


  discountId: number;
  discount: Discount;

  shopId: number;
  shop: Shop; //Migration

  additionalFeeId: number;
  additionalFee: AdditionalFee; //Migration

  products: Product[];  //Migration
  attributs: Array<Attribut>;
  partners: Partner[];
}

export class Partner {
  id: number;
  email: string;

  categories: Category[];
}

export class City {
  id: number;
  name: string;

  regionId: number;
  region: Region;

  deliveries: Delivery[];     //Migration
  stores: Store[];    //Migration
}

export class Customer {
  id: number;
  firstname: string;
  lastname: string;
  email: string;
  phone: string;
  subscriptionDate: Date;
  cashback: number;

  //cart:Cart;

  addresses: Array<_Address>;
  shopCustomerOrders: ShopCustomerOrder[];//Migration
  products: Product[];//Migration
  reviews: Review[];//Migration maybe
}

export class DeliveryMode {
  id: number;
  value: number;
  deliveries: Delivery[];//Migration
}

export class DeliveryType {
  id: number;
  value: number;
  shops: Shop[];//Migration

}

export class Delivery {
  id: number;
  downtown: boolean;
  from: number;
  to: number;
  freeFrom: number;
  radius: number;
  minOrderPrice: number;
  price: number;
  deliveryTime: number;

  deliveryModeId: number;
  deliveryMode: DeliveryMode;

  shopId: number;
  shop: Shop;//Migration

  cities: City[];
}

export class DiscountType {
  id: number;
  value: number;
  discounts: Discount[];//Migration
}

export class Discount {
  id: number;
  name: string;
  description: string;
  amount: number;
  percent: number;
  startDate: Date;
  endDate: Date;
  code: string;
  affectations: string;

  shops: Shop[];//Migration

  affectationTypeId: number;
  affectationType: AffetationType;

  discountTypeId: number;
  discountType: DiscountType;

  products: Product[];//Migration
  categories: Category[];//Migration

}

export class Language {
  id: number;
  value: string;

  userCredentials: UserCredential[];//Migration

}

export class OpeningTime {
  id: number;
  day: string;
  startTime: Date;
  endTime: Date;

  shopId: number;
  shop: Shop;//Migration

}

export class OrderStatus {
  id: number;
  value: string;
  sequence: number;
  orders: Order[];//Migration

}

export class Order {
  id: number;
  orderDate: Date;
  deliveryDate: Date;
  price: number;
  deliveryPrice: number;
  active: boolean;
  number: string;
  editDate: Date;
  cashback: number;
  usedCashback: number;
  usedDiscountCode: string;

  cartId: number;
  cart: Cart;

  addressId: number;
  address: _Address;

  deliveryManId: number;
  deliveryMan: DeliveryMan;

  paymentStatusId: number;
  paymentStatus: PaymentStatus;

  orderStatusId: number;
  orderStatus: OrderStatus;

  paymentMethodId: string;
  paymentMethod: PaymentMethod;

  actions: Array<Action>;
  shopCustomerOrders: ShopCustomerOrder[];//Migration
}

export class PaymentMethod {
  id: number;
  type: string;
  active: boolean;

  shops: Shop[]; //Migration
  orders: Order[]; //Migration
}


export class PaymentStatus {
  id: number;
  value: string;
  orders: Order[];//Migration
}

export class Picture {
  id: number;
  url: string;
  urlTn: string;

  productId: number;
  product: Product;//Migration
}

export class ProductAttributsVariant {
  id: number;
  attributids: string;
  productId: number;
  variantId: number;

  variant: Variant;
  product: Product;//Migration
}

export class ProductVariantCart {
  id: number;
  logoUrl: string;
  quantity: number;

  selectedAttributItems: string;
  selectedVariants: string;
  discount: string;
  product: string;

  cartId: number;
  cart: Cart;//Migration
}

export class Product {
  id: number;
  name: string;
  description: string;
  price: number;
  available: boolean;
  stock: number;
  suggestOnly: boolean;
  ddOrder: number;

  parrentProductId: string;
  parrentProduct: Product;//Migration
  childreProducts: Product[];

  frontLine: boolean;
  slug: string;
  metaTitle: string;
  metaDescription: string;
  saleCount: number;

  creationDate: Date;
  editDate: Date;
  published: boolean;
  publisher: number;
  externalId: number;
  discountPrice: number;

  brandId: number;
  brand: Brand;

  discountId: string;
  discount: Discount;

  shopId: number;
  shop: Shop;//Migration

  pictures: Array<Picture>;
  attributs: Array<Attribut>;
  categories: Array<Category>;

  productAttributVariants: ProductAttributsVariant[];//Migration
  customer: Customer[];//Migration
  reviews: Review[];//Migration maybe
}

export class Region {
  id: number;
  name: string;

  cities: City[];//Migration
}

export class Review {
  id: number;
  score: number;
  comment: string;

  customerId: number;
  customer: Customer;//Migration

  productId: number;
  product: Product;//Migration maybe
}

export class ShopCustomerOrder {
  id: number;
  date: string;

  customerId: number;
  customer: Customer;

  shopId: number;
  shop: Shop;

  orderId: number;
  order: Order;
}

export class Shop {
  id: number;
  companyName: string;
  socialName: string;
  description: string;
  subdomain: string;
  email: string;
  logoUrl: string;
  headerUrl: string;
  reimbursmentPolitic: string;
  alwaysOpen: boolean;
  preorder: boolean;
  review: boolean;
  satisfactionPolitic: string;
  warranty: string;
  active: boolean;
  list: boolean;
  grid: boolean;
  takeAway: boolean;
  productLimit: number;
  pictureLimit: number;
  creationDate: Date;
  editDate: Date;

  phone: string;
  facebook: string;
  whatsapp: string;

  activityId: number;
  activity: Activity;

  deliveryTypeId: number;
  deliveryType: DeliveryType; //?

  cashbackId: number;
  cashback: Cashback;

  // one of them
  // unit:Unit;
  units: Array<Unit>;//Migration

  // one to one
  discountId: number;
  discount: Discount;

  openingTimes: Array<OpeningTime>;
  categories: Array<Category>;
  products: Array<Product>;
  stores: Array<Store>; 
  deliveryMans: Array<DeliveryMan>;
  deliveries: Array<Delivery>;
  paymentMethods: Array<PaymentMethod>;
  shopCustomerOrders: ShopCustomerOrder;
}

export class DeliveryMan {
  id: number;
  name: string;
  email: string;
  tel: string;

  orders: Order[];
  shops: Shop[];//Migration
}

export class Store {
  id: number;
  name: string;
  phone: string;
  email: string;
  address: string;
  gps: string;

  shopId: number;
  shop: Shop;//Migration

  cityId: number;
  city: City;
}

export class Ticket {
  id: number;
  subject: string;
  message: string;
  department: string;
  priority: string;
  status: string;
  unread: boolean;
  shopId: string;
}

export class Unit {
  id: number;
  weight: string;
  dimension: string;

  shopId: number;
  shop: Shop;//Migration
}

export class UserCredential {
  id: number;
  email: string;
  password: string;
  username: string;
  lastname: string;
  firstname: string;
  token: string;

  //Credentials
  admin: boolean;
  shop: boolean;
  customer: boolean;
  deliveryMan: boolean;
  phone: string;
  reseller: boolean;
  approvingShop: boolean;
  partner: boolean;
  //End Credentials 

  languageId: number;
  language: Language;
}

export class Variant {
  id: number;
  name: string;
  description: string;
  addedPrice: number;
  price: number;
  available: boolean;
  stock: number;
  ddOrder: number;
  items: string;

  attributId: number;
  attribut: Attribut;//Migration

  productAttributsVariants: ProductAttributsVariant[];//Migration
}








