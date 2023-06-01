import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class CommonService {
  public static readonly ADD_TO_CART_QUANTITY = 'quantity';
  public static readonly ADD_TO_CART_IS_CONFIGURABLE = 'is_configurable';
  public static readonly ADD_TO_CART_PRODUCT_ID = 'product_id';
  public static readonly ADD_TO_CART_SELECTED_CONFIGURABLE_OPTION =
    'selected_configurable_option';
  public static readonly ADD_TO_CART_SUPER_ATTRIBUTE = 'super_attribute';
  public static readonly ADD_TO_CART_DEFAULT_VARIATION: number = 0;

  constructor() {}

  public static getProductDataForAddToCart(product: any, qty = 1): any {
    const data = {};
    if (product.type == 'simple') {
      data[CommonService.ADD_TO_CART_QUANTITY] = qty;
      data[CommonService.ADD_TO_CART_IS_CONFIGURABLE] = false;
      data[CommonService.ADD_TO_CART_PRODUCT_ID] = product.id;
    } else {
      const res = {};
      product.super_attributes.forEach((attr) => {
        res[attr.id] = product[attr.code];
      });
      data[CommonService.ADD_TO_CART_QUANTITY] = qty;
      data[CommonService.ADD_TO_CART_IS_CONFIGURABLE] = true;
      data[CommonService.ADD_TO_CART_PRODUCT_ID] = product.id;
      data[CommonService.ADD_TO_CART_SELECTED_CONFIGURABLE_OPTION] =
        product.variants[CommonService.ADD_TO_CART_DEFAULT_VARIATION].id;
      data[CommonService.ADD_TO_CART_SUPER_ATTRIBUTE] = res;
    }
    return data;
  }
}
