export interface Welcome {
  data?: Product[];
  links?: Links;
  meta?: Meta;
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
  variants?: Variant[];
  super_attributes?: SuperAttribute[];
}

export interface BaseImage {
  small_image_url?: string;
  medium_image_url?: string;
  large_image_url?: string;
  original_image_url?: string;
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

export interface Reviews {
  total?: number;
  total_rating?: Rating;
  average_rating?: Rating;
  percentage?: Percentage;
}

export type Rating = number | string;

export type Percentage = any[] | string;

export interface SuperAttribute {
  id?: number;
  code?: string;
  type?: string;
  name?: string;
  swatch_type?: null;
  options?: Option[];
  created_at?: Date;
  updated_at?: Date;
}

export interface Option {
  id?: number;
  admin_name?: string;
  label?: string;
  swatch_value?: null;
}

export interface Variant {
  id?: number;
  sku?: string;
  type?: Type;
  created_at?: Date;
  updated_at?: Date;
  parent_id?: number;
  attribute_family_id?: number;
  short_description?: null;
  description?: null;
  name?: string;
  url_key?: null;
  tax_category_id?: null;
  new?: null;
  featured?: null;
  visible_individually?: null;
  status?: number;
  color?: number;
  size?: number;
  brand?: null;
  guest_checkout?: null;
  product_number?: null;
  meta_title?: null;
  meta_keywords?: null;
  meta_description?: null;
  price?: string;
  cost?: null;
  special_price?: null;
  special_price_from?: null;
  special_price_to?: null;
  width?: null;
  height?: null;
  depth?: null;
  weight?: string;
  inventories?: Inventory[];
  attribute_family?: AttributeFamily;
}

export interface AttributeFamily {
  id?: number;
  code?: string;
  name?: string;
  status?: number;
  is_user_defined?: number;
}

export interface Inventory {
  id?: number;
  qty?: number;
  product_id?: number;
  inventory_source_id?: number;
  vendor_id?: number;
}

export enum Type {
  Simple = 'simple',
}

export interface Links {
  first?: string;
  last?: string;
  prev?: null;
  next?: null;
}

export interface Meta {
  current_page?: number;
  from?: number;
  last_page?: number;
  links?: Link[];
  path?: string;
  per_page?: number;
  to?: number;
  total?: number;
}

export interface Link {
  url?: null | string;
  label?: string;
  active?: boolean;
}
