export interface Cart {
  id?: number;
  customer_email?: null;
  customer_first_name?: null;
  customer_last_name?: null;
  shipping_method?: null;
  coupon_code?: null;
  is_gift?: number;
  items_count?: number;
  items_qty?: string;
  exchange_rate?: null;
  global_currency_code?: string;
  base_currency_code?: string;
  channel_currency_code?: string;
  cart_currency_code?: string;
  grand_total?: string;
  formated_grand_total?: string;
  base_grand_total?: string;
  formated_base_grand_total?: string;
  sub_total?: string;
  formated_sub_total?: string;
  base_sub_total?: string;
  formated_base_sub_total?: string;
  tax_total?: string;
  formated_tax_total?: string;
  base_tax_total?: string;
  formated_base_tax_total?: string;
  discount?: string;
  formated_discount?: string;
  base_discount?: string;
  formated_base_discount?: string;
  checkout_method?: null;
  is_guest?: number;
  is_active?: number;
  conversion_time?: null;
  channel?: null;
  items?: Item[];
  selected_shipping_rate?: null;
  payment?: null;
  billing_address?: null;
  shipping_address?: null;
  created_at?: Date;
  updated_at?: Date;
  taxes?: string;
  formated_taxes?: string;
  base_taxes?: string;
  formated_base_taxes?: string;
  formated_discounted_sub_total?: string;
  formated_base_discounted_sub_total?: string;
}

export interface Item {
  id?: number;
  quantity?: number;
  sku?: string;
  type?: string;
  name?: string;
  coupon_code?: null;
  weight?: string;
  total_weight?: string;
  base_total_weight?: string;
  price?: string;
  formated_price?: string;
  base_price?: string;
  formated_base_price?: string;
  custom_price?: null;
  formated_custom_price?: string;
  total?: string;
  formated_total?: string;
  base_total?: string;
  formated_base_total?: string;
  tax_percent?: string;
  tax_amount?: string;
  formated_tax_amount?: string;
  base_tax_amount?: string;
  formated_base_tax_amount?: string;
  discount_percent?: string;
  discount_amount?: string;
  formated_discount_amount?: string;
  base_discount_amount?: string;
  formated_base_discount_amount?: string;
  additional?: Additional;
  child?: null;
  product?: Product;
  created_at?: Date;
  updated_at?: Date;
}

export interface Additional {
  quantity?: number;
  is_configurable?: boolean;
  token?: string;
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
  images?: Image[];
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
}

export interface BaseImage {
  small_image_url?: string;
  medium_image_url?: string;
  large_image_url?: string;
  original_image_url?: string;
}

export interface Reviews {
  total?: number;
  total_rating?: string;
  average_rating?: string;
  percentage?: string;
}

export interface Image {
  id?: number;
  path?: string;
  url?: string;
  original_image_url?: string;
  small_image_url?: string;
  medium_image_url?: string;
  large_image_url?: string;
}
