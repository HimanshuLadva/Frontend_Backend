export interface CategoryTree {
  id?: number;
  name?: string;
  slug?: string;
  url_path?: string;
  parent_id?: number;
  children?: CategoryTree[];
}
