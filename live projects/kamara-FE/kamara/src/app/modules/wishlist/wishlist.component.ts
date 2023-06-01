import { Title } from '@angular/platform-browser';
import {Component, OnInit} from '@angular/core';
import {WishlistService} from '@services/wishlist.service';
import {AuthService} from '@auth/service/auth.service';
import {Wishlist} from '@modals/wishlist.modal';
import {CommonService} from '@services/common.service';
import {CartService} from '@services/cart.service';
import {EventBusEmitService} from '@shared/service/event-bus-emit.service';
import {RouteConfig} from '@shared/config/route-config';

@Component({
  selector: 'app-wishlist',
  templateUrl: './wishlist.component.html',
  styleUrls: ['./wishlist.component.scss']
})
export class WishlistComponent implements OnInit {
  RouteConfig = RouteConfig;
  wishlistProducts: Wishlist[] = [];
  isLoading = true;

  constructor(
    private authService: AuthService,
    private wishlistService: WishlistService,
    private cartService: CartService,
    private eventBusEmitService: EventBusEmitService,
    private title: Title,
  ) {
  }

  ngOnInit(): void {
    this.loadWishlist();
    this.title.setTitle("My WishList");
    
  }

  async loadWishlist(): Promise<void> {
    try {
      this.eventBusEmitService.showLoading();
      const res = await this.wishlistService.getCustomerWishlistProduct(this.authService.User.id);
      if (res?.data?.length > 0) {
        this.wishlistProducts = res.data;
      }
    } catch (e) {
      console.error('::::AAA::::BBB::::', e);
    } finally {
      this.eventBusEmitService.hideLoading();
      this.isLoading = false;
    }
  }

  async removeFromWishlist(productId): Promise<void> {
    try {
      this.eventBusEmitService.showLoading();
      const res = await this.wishlistService.removeProductFromWishlist(productId);
      if (res.error) {
        this.eventBusEmitService.showMessage({msg: res.error.message});
      } else {
        this.eventBusEmitService.showMessage({msg: res.message});
      }
      if (res) {
        // this.ngOnInit();
        await this.loadWishlist();
      }
    } catch (e) {
      console.error('::::AAA::::BBB::::', e);
    } finally {
      this.eventBusEmitService.hideLoading();
      this.isLoading = false;
    }
  }

  async addToCart(index: number): Promise<void> {
    if (!this.wishlistProducts[index].product.in_stock) {
      return;
    }
    try {
      this.eventBusEmitService.showLoading();
      const res = await this.cartService.addProduct(this.wishlistProducts[index].product.id, CommonService.getProductDataForAddToCart(this.wishlistProducts[index].product));
      if (res.error) {
        this.eventBusEmitService.showMessage({msg: res.error.message});
      } else {
        this.eventBusEmitService.showMessage({msg: res.message});
      }
      if (res) {
        await this.loadWishlist();
      }
    } catch (e) {
      console.error('::::AAA::::BBB::::', e);
    } finally {
      this.eventBusEmitService.hideLoading();
    }
  }

}
