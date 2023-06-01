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

export interface Image {
  id?: number;
  url?: string;
  path?: string;
  small_image_url?: string;
  medium_image_url?: string;
  large_image_url?: string;
  original_image_url?: string;
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

export interface SuperAttribute {
  id?: number;
  code?: string;
  type?: string;
  name?: string;
  swatch_type?: null | string;
  options?: Option[];
  created_at?: Date;
  updated_at?: Date;
}

export interface Option {
  id?: number;
  admin_name?: string;
  label?: string;
  swatch_value?: null;
  image_full_path?: null;
}

export interface Variant {
  id?: number;
  sku?: string;
  type?: string;
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
  metal?: null;
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

export interface SelectedSuperAttribute {
  code?: string;
  value?: any;
}
export enum productSliderType {
  'featured',
  'best_seller',
  'complete_the_look_products',
  'similar_design_products',
  'related_products',
}
