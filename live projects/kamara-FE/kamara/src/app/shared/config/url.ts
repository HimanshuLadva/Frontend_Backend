import { environment } from '../../../environments/environment';

export class Urls {
  static baseUrl = environment.apiUrl;
  static baseMainUrl = environment.url;
  static login = environment.apiUrl + 'customer/login?token=true';
  static logout = environment.apiUrl + 'customer/logout?token=true';
  static register = environment.apiUrl + 'customer/register';
  static customer = environment.apiUrl + 'customer';
  static address = environment.apiUrl + 'addresses';
  static get_city_state_from_postcode =
    environment.apiUrl + 'fetchStateAndCityByPincode';
  static country = environment.apiUrl + 'countries?limit=255';
  static homepageImages = environment.apiUrl + 'homepage';
  static products = environment.apiUrl + 'products';
  static categories = environment.apiUrl + 'categories';
  static checkout = environment.apiUrl + 'checkout';
  static wishlist = environment.apiUrl + 'wishlist';
  static filterAttributes =
    environment.apiUrl + 'getFilterAttributeByCategoryId';
  static filterAttributesSlug =
    environment.apiUrl + 'getFilterAttributeByCategorySlug';
  static productSearchSuggestions =
    environment.apiUrl + 'productSearchSuggestions';
  static productMoreDetail =
    environment.apiUrl + 'product-additional-information';
  static orders = environment.apiUrl + 'orders';
  static productBySlug = environment.apiUrl + 'product-by-slug';
  static productSimilarDesign = environment.apiUrl + 'product-up-sell';
  static productCompleteTheLook = environment.apiUrl + 'product-cross-sell';
}
