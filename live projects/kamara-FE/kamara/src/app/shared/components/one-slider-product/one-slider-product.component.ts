import {Component, Input, OnInit} from '@angular/core';
import {Product} from '@modals/product-slider.modal';
import {EventBusEmitService} from '../../service/event-bus-emit.service';
import {CartService} from '@services/cart.service';
import {WishlistService} from '@services/wishlist.service';
import {AuthService} from '@auth/service/auth.service';
import {CommonService} from '@services/common.service';
import {RouteConfig} from '@shared/config/route-config';

@Component({
  selector: 'app-one-slider-product',
  templateUrl: './one-slider-product.component.html',
  styleUrls: ['./one-slider-product.component.scss']
})
export class OneSliderProductComponent implements OnInit {
  RouteConfig = RouteConfig;

  @Input() product: Product;


  constructor(
    private eventBusEmitService: EventBusEmitService,
    private cartService: CartService,
    private wishlistService: WishlistService,
    public authService: AuthService
  ) {
  }

  ngOnInit(): void {
  }

  showQuickViewModal(): void {
    this.eventBusEmitService.showQuickViewModal(this.product.id);
  }

  async addToCart(): Promise<void> {
    try {
      const res = await this.cartService.addProduct(this.product.id, CommonService.getProductDataForAddToCart(this.product));
      if (res.error) {
        this.eventBusEmitService.showMessage({msg: res.error.message});
      } else {
        this.eventBusEmitService.showMessage({msg: res.message});
      }
    } catch (e) {
      console.error('::::AAA::::BBB::::', e);
    }
  }

  async addToWishlist(id: number): Promise<void> {
    try {
      const res = await this.wishlistService.addProductToWishlist(id);
      if (res.error) {
        this.eventBusEmitService.showMessage({msg: res.error.message});
      } else {
        this.eventBusEmitService.showMessage({msg: res.message});
      }
    } catch (e) {
      console.error('::::AAA::::BBB::::', e);
    }
  }

  private getSuperAttribute(): any {
    const res = {};
    const product = this.product.variants[0];
    this.product.super_attributes.forEach((attr) => {
      res[attr.id] = product[attr.code];
    });
    return res;
  }
}
