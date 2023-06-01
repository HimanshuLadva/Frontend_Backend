import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Urls } from '@shared/config/url';
import { EventBusEmitService } from '@shared/service/event-bus-emit.service';
import { AuthService } from '@auth/service/auth.service';
import { Static } from '@shared/static/static';
import { ActivatedRouteSnapshot, Router } from '@angular/router';
import { RouteConfig } from '@shared/config/route-config';

@Injectable({
  providedIn: 'root',
})
export class CartService {
  constructor(
    private http: HttpClient,
    private eventBusEmitService: EventBusEmitService,
    private authService: AuthService,
    private router: Router
  ) {}

  addProduct(productId, data): Promise<any> {
    return new Promise((resolve, reject) => {
      if (this.authService.isUserGuest) {
        this.seeForGuest({ productId, data });
        return;
      }
      this.http
        .post<any>(`${Urls.checkout}/cart/add/${productId}?token=true`, data)
        .subscribe((res) => {
          if (res) {
            this.eventBusEmitService.reloadCartCount();
            resolve(res);
          }
        }, reject);
    });
  }

  seeForGuest(res: any): void {
    // localStorage.setItem(Static.GUEST_CART_STORAGE_KEY, JSON.stringify(res));
    this.router.navigate([RouteConfig.auth_login], {
      queryParams: { returnUrl: document.location.pathname },
    });
  }
  // async seeForLoginUser(): Promise<any> {
  //   return new Promise((resolve, reject) => {
  //     if (localStorage.getItem(Static.GUEST_CART_STORAGE_KEY)) {
  //       const { productId, data } = JSON.parse(
  //         localStorage.getItem(Static.GUEST_CART_STORAGE_KEY)
  //       );
  //       localStorage.removeItem(Static.GUEST_CART_STORAGE_KEY);
  //       this.addProduct(productId, data).then(resolve).catch(reject);
  //     } else {
  //       resolve(null);
  //     }
  //   });
  // }

  getAllProductFromCart(): Promise<any> {
    return new Promise((resolve, reject) => {
      this.http
        .get<any>(`${Urls.checkout}/cart?token=true`)
        .subscribe(resolve, reject);
    });
  }

  getCartDetailForCheckout(): Promise<any> {
    return new Promise((resolve, reject) => {
      this.http
        .get<any>(`${Urls.checkout}/cart?token=true`)
        .subscribe(resolve, reject);
    });
  }

  getAllProductCountFromCart(): Promise<any> {
    return new Promise((resolve, reject) => {
      this.http.get<any>(`${Urls.checkout}/cart?token=true`).subscribe(
        (res) =>
          resolve(res?.data?.items_count > 0 ? res?.data?.items_count : 0),
        (err) => resolve(0)
      );
    });
  }

  emptyCart(): Promise<any> {
    return new Promise((resolve, reject) => {
      this.http
        .get<any>(`${Urls.checkout}/cart/empty?token=true`)
        .subscribe((res) => {
          if (res) {
            this.eventBusEmitService.reloadCartCount();
            resolve(res);
          }
        }, reject);
    });
  }

  removeCartProduct(cartItemId): Promise<any> {
    return new Promise((resolve, reject) => {
      this.http
        .get<any>(`${Urls.checkout}/cart/remove-item/${cartItemId}?token=true`)
        .subscribe((res) => {
          if (res) {
            this.eventBusEmitService.reloadCartCount();
            resolve(res);
          }
        }, reject);
    });
  }

  updateCartProductQty(data): Promise<any> {
    return new Promise((resolve, reject) => {
      this.http
        .put<any>(`${Urls.checkout}/cart/update?token=true`, data)
        .subscribe((res) => {
          if (res) {
            this.eventBusEmitService.reloadCartCount();
            resolve(res);
          }
        }, reject);
    });
  }
}
