export class RouteConfig {
  public static base = '/';
  public static customer = '';
  public static auth = '/auth';
  public static auth_login = RouteConfig.auth + '/login';
  public static auth_register = RouteConfig.auth + '/register';
  public static auth_account = RouteConfig.auth + '/account';
  public static home = RouteConfig.customer;
  public static wishlist = RouteConfig.customer + '/wishlist';
  public static cart = RouteConfig.customer + '/cart';
  public static list = RouteConfig.customer + '/category';
  public static searchProduct = RouteConfig.customer + '/search';
  public static checkout = RouteConfig.customer + '/checkout';
  public static orderDetail = RouteConfig.customer + '/order-detail';
  public static productDetail = RouteConfig.customer + '/product';
  public static aboutUs = RouteConfig.customer + '/about-us';
  public static policy = RouteConfig.customer + '/privacy-policy';
}
