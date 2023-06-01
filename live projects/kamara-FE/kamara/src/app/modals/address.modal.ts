export interface AddressModal {
  address1?: string[];
  city?: string;
  country?: string;
  country_name?: string;
  phone?: string;
  postcode?: string;
  state?: string;
}

export interface Country {
  id?: number;
  code?: string;
  name?: string;
}

export interface State {
  id?: number;
  code?: string;
  country_code?: string;
  name?: string;
  country_id?: number;
}

export interface Pincode {
  pincode?: string;
  district?: string;
  state?: string;
  state_code?: string;
  state_id?: number;
  country_id?: number;
}


export interface AddressDetail {
  id?: number;
  first_name?: string;
  last_name?: string;
  company_name?: null;
  vat_id?: null;
  address1?: string[];
  country?: string;
  country_name?: string;
  state?: string;
  city?: string;
  postcode?: string;
  phone?: string;
  is_default?: boolean;
  created_at?: Date;
  updated_at?: Date;
}
