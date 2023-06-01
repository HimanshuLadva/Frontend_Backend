import { Pagination } from '@modals/pagination.modal';

export class Static {
  public static PRODUCT_LIST_PAGINATION_OBJ: Pagination = {
    current_page: 1,
    per_page: 9,
  };
  public static PRODUCT_SEARCH_LIST_PAGINATION_OBJ: Pagination = {
    current_page: 1,
    per_page: 12,
  };
  public static FILTER_ATTRIBUTE_SHOW_COUNT = 5;
  public static FILTER_CATEGORY_SHOW_COUNT = 5;
  public static FILTER_CATEGORY_DEFAULT_SELECTED_INDEX = 1;
  public static PRODUCT_DETAIL_DEFAULT_ID = 288;
  public static DEFAULT_PAYMENT_METHOD_DATA = {
    payment: {
      method: 'cashondelivery',
    },
  };
  public static DEFAULT_SHIPPING_METHOD_DATA = { shipping_method: 'free_free' };
  public static GUEST_CART_STORAGE_KEY = 'GUEST_CART';
}
