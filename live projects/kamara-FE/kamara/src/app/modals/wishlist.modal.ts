export interface Wishlist {
  id?: number;
  product?: Product;
  created_at?: Date;
  updated_at?: Date;
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
