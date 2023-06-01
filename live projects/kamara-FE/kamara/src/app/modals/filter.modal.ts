export interface FilterResponse {
  id?: number;
  position?: number;
  image?: null;
  status?: number;
  _lft?: number;
  _rgt?: number;
  parent_id?: number;
  created_at?: Date;
  updated_at?: Date;
  display_mode?: string;
  category_icon_path?: null;
  additional?: null;
  name?: string;
  description?: string;
  slug?: string;
  url_path?: string;
  meta_title?: string;
  meta_description?: string;
  meta_keywords?: string;
  translations?: DataTranslation[];
  filterable_attributes?: FilterableAttribute[];
}

export interface FilterableAttribute {
  id?: number;
  code?: string;
  admin_name?: string;
  type?: string;
  validation?: null | string;
  position?: number;
  is_required?: number;
  is_unique?: number;
  value_per_locale?: number;
  value_per_channel?: number;
  is_filterable?: number;
  is_configurable?: number;
  is_user_defined?: number;
  is_visible_on_front?: number;
  created_at?: Date;
  updated_at?: Date;
  swatch_type?: null;
  use_in_flat?: number;
  is_comparable?: number;
  name?: string;
  pivot?: Pivot;
  options?: Option[];
  translations?: FilterableAttributeTranslation[];
}

export interface Option {
  id?: number;
  admin_name?: string;
  sort_order?: number;
  attribute_id?: number;
  swatch_value?: null;
  label?: string;
  translations?: OptionTranslation[];
}

export interface OptionTranslation {
  id?: number;
  locale?: string;
  label?: string;
  attribute_option_id?: number;
}

export interface Pivot {
  category_id?: number;
  attribute_id?: number;
}

export interface FilterableAttributeTranslation {
  id?: number;
  locale?: string;
  name?: string;
  attribute_id?: number;
}

export interface DataTranslation {
  id?: number;
  name?: string;
  slug?: string;
  description?: string;
  meta_title?: string;
  meta_description?: string;
  meta_keywords?: string;
  category_id?: number;
  locale?: string;
  locale_id?: number;
  url_path?: string;
}
