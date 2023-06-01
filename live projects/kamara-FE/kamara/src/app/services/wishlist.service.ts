import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Urls } from '@shared/config/url';
import { EventBusEmitService } from '@shared/service/event-bus-emit.service';

@Injectable({
  providedIn: 'root',
})
export class WishlistService {
  constructor(
    private http: HttpClient,
    private eventBusEmitService: EventBusEmitService
  ) {}

  getCustomerWishlistProduct(customerId): Promise<any> {
    return new Promise((resolve, reject) => {
      this.http
        .get<any>(`${Urls.wishlist}?customer_id=${customerId}&token=true`)
        .subscribe(resolve, reject);
    });
  }

  getCustomerWishlistProductCount(customerId): Promise<any> {
    return new Promise((resolve, reject) => {
      this.http
        .get<any>(
          `${Urls.wishlist}?customer_id=${customerId}&token=true&limit=1`
        )
        .subscribe(
          (res) => resolve(res?.meta?.total > 0 ? res?.meta?.total : 0),
          (err) => resolve(0)
        );
    });
  }

  addProductToWishlist(productId): Promise<any> {
    return new Promise((resolve, reject) => {
      this.http
        .get<any>(`${Urls.wishlist}/add/${productId}?token=true`)
        .subscribe((res) => {
          if (res) {
            this.eventBusEmitService.reloadWishlistCount();
            resolve(res);
          }
        }, reject);
    });
  }

  removeProductFromWishlist(productId): Promise<any> {
    return this.addProductToWishlist(productId);
  }
}
