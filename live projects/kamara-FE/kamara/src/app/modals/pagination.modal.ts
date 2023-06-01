export interface Pagination {
  current_page?: number;
  from?: null;
  last_page?: number;
  links?: Link[];
  path?: string;
  per_page?: number;
  to?: null;
  total?: number;
}

export interface Link {
  url?: null | string;
  label?: string;
  active?: boolean;
}
