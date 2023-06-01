export interface Order {
  id?: number;
  increment_id?: string;
  status?: string;
  status_label?: string;
  channel_name?: string;
  is_guest?: number;
  customer_email?: string;
  customer_first_name?: string;
  customer_last_name?: string;
  shipping_method?: string;
  shipping_title?: string;
  payment_title?: string;
  shipping_description?: string;
  coupon_code?: null;
  is_gift?: number;
  total_item_count?: number;
  total_qty_ordered?: number;
  base_currency_code?: string;
  channel_currency_code?: string;
  order_currency_code?: string;
  grand_total?: string;
  formated_grand_total?: string;
  base_grand_total?: string;
  formated_base_grand_total?: string;
  grand_total_invoiced?: string;
  formated_grand_total_invoiced?: string;
  base_grand_total_invoiced?: string;
  formated_base_grand_total_invoiced?: string;
  grand_total_refunded?: string;
  formated_grand_total_refunded?: string;
  base_grand_total_refunded?: string;
  formated_base_grand_total_refunded?: string;
  sub_total?: string;
  formated_sub_total?: string;
  base_sub_total?: string;
  formated_base_sub_total?: string;
  sub_total_invoiced?: string;
  formated_sub_total_invoiced?: string;
  base_sub_total_invoiced?: string;
  formated_base_sub_total_invoiced?: string;
  sub_total_refunded?: string;
  formated_sub_total_refunded?: string;
  discount_percent?: string;
  discount_amount?: string;
  formated_discount_amount?: string;
  base_discount_amount?: string;
  formated_base_discount_amount?: string;
  discount_invoiced?: string;
  formated_discount_invoiced?: string;
  base_discount_invoiced?: string;
  formated_base_discount_invoiced?: string;
  discount_refunded?: string;
  formated_discount_refunded?: string;
  base_discount_refunded?: string;
  formated_base_discount_refunded?: string;
  tax_amount?: string;
  formated_tax_amount?: string;
  base_tax_amount?: string;
  formated_base_tax_amount?: string;
  tax_amount_invoiced?: string;
  formated_tax_amount_invoiced?: string;
  base_tax_amount_invoiced?: string;
  formated_base_tax_amount_invoiced?: string;
  tax_amount_refunded?: string;
  formated_tax_amount_refunded?: string;
  base_tax_amount_refunded?: string;
  formated_base_tax_amount_refunded?: string;
  shipping_amount?: string;
  formated_shipping_amount?: string;
  base_shipping_amount?: string;
  formated_base_shipping_amount?: string;
  shipping_invoiced?: string;
  formated_shipping_invoiced?: string;
  base_shipping_invoiced?: string;
  formated_base_shipping_invoiced?: string;
  shipping_refunded?: string;
  formated_shipping_refunded?: string;
  base_shipping_refunded?: string;
  formated_base_shipping_refunded?: string;
  customer?: Customer;
  channel?: Channel;
  shipping_address?: OrderAddress;
  billing_address?: OrderAddress;
  items?: Item[];
  invoices?: any[];
  shipments?: any[];
  updated_at?: Date;
  created_at?: Date;
}

export interface OrderAddress {
  id?: number;
  email?: string;
  first_name?: string;
  last_name?: string;
  address1?: string[];
  country?: string;
  country_name?: string;
  state?: string;
  city?: string;
  postcode?: string;
  phone?: string;
  created_at?: Date;
  updated_at?: Date;
}

export interface Channel {
  id?: number;
  code?: string;
  name?: string;
  description?: string;
  timezone?: null;
  theme?: string;
  home_page_content?: string;
  footer_content?: string;
  hostname?: string;
  logo?: null;
  logo_url?: null;
  favicon?: null;
  favicon_url?: null;
  default_locale?: DefaultLocale;
  root_category_id?: number;
  root_category?: RootCategory;
  created_at?: null;
  updated_at?: Date;
}

export interface DefaultLocale {
  id?: number;
  code?: string;
  name?: string;
  created_at?: null;
  updated_at?: null;
}

export interface RootCategory {
  id?: number;
  code?: null;
  name?: string;
  slug?: string;
  display_mode?: string;
  description?: string;
  meta_title?: string;
  meta_description?: string;
  meta_keywords?: string;
  status?: number;
  image_url?: null;
  additional?: null;
  created_at?: Date;
  updated_at?: Date;
}

export interface Customer {
  id?: number;
  email?: string;
  first_name?: string;
  last_name?: string;
  name?: string;
  gender?: string;
  date_of_birth?: Date;
  phone?: string;
  status?: number;
  group?: DefaultLocale;
  created_at?: Date;
  updated_at?: Date;
  profile_picture?: string;
}

export interface Item {
  id?: number;
  sku?: string;
  type?: string;
  name?: string;
  product?: Product;
  coupon_code?: null;
  weight?: string;
  total_weight?: string;
  qty_ordered?: number;
  qty_canceled?: number;
  qty_invoiced?: number;
  qty_shipped?: number;
  qty_refunded?: number;
  price?: string;
  formated_price?: string;
  base_price?: string;
  formated_base_price?: string;
  total?: string;
  formated_total?: string;
  base_total?: string;
  formated_base_total?: string;
  total_invoiced?: string;
  formated_total_invoiced?: string;
  base_total_invoiced?: string;
  formated_base_total_invoiced?: string;
  amount_refunded?: string;
  formated_amount_refunded?: string;
  base_amount_refunded?: string;
  formated_base_amount_refunded?: string;
  discount_percent?: string;
  discount_amount?: string;
  formated_discount_amount?: string;
  base_discount_amount?: string;
  formated_base_discount_amount?: string;
  discount_invoiced?: string;
  formated_discount_invoiced?: string;
  base_discount_invoiced?: string;
  formated_base_discount_invoiced?: string;
  discount_refunded?: string;
  formated_discount_refunded?: string;
  base_discount_refunded?: string;
  formated_base_discount_refunded?: string;
  tax_percent?: string;
  tax_amount?: string;
  formated_tax_amount?: string;
  base_tax_amount?: string;
  formated_base_tax_amount?: string;
  tax_amount_invoiced?: string;
  formated_tax_amount_invoiced?: string;
  base_tax_amount_invoiced?: string;
  formated_base_tax_amount_invoiced?: string;
  tax_amount_refunded?: string;
  formated_tax_amount_refunded?: string;
  base_tax_amount_refunded?: string;
  formated_base_tax_amount_refunded?: string;
  grant_total?: number;
  formated_grant_total?: string;
  base_grant_total?: number;
  formated_base_grant_total?: string;
  downloadable_links?: any[];
  additional?: Additional;
  child?: null;
  children?: any[];
}

export interface Additional {
  quantity?: number;
  is_configurable?: boolean;
  product_id?: number;
  token?: string;
  locale?: string;
}

export interface Product {
  id?: number;
  sku?: string;
  type?: string;
  name?: string;
  url_key?: string;
  price?: string;
  formated_price?: string;
  short_description?: string;
  description?: string;
  images?: any[];
  videos?: any[];
  base_image?: BaseImage;
  created_at?: Date;
  updated_at?: Date;
  reviews?: Reviews;
  in_stock?: boolean;
  is_saved?: boolean;
  is_wishlisted?: boolean;
  is_item_in_cart?: boolean;
  show_quantity_changer?: boolean;
  special_price?: string;
  formated_special_price?: string;
  regular_price?: string;
  formated_regular_price?: string;
}

export interface BaseImage {
  small_image_url?: string;
  medium_image_url?: string;
  large_image_url?: string;
  original_image_url?: string;
}

export interface Reviews {
  total?: number;
  total_rating?: number;
  average_rating?: number;
  percentage?: any[];
}

