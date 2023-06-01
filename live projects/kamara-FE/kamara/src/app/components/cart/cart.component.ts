import {Component, OnInit} from '@angular/core';
import {EventBusService} from '@shared/service/event-bus/event-bus.service';
import {AuthService} from '@auth/service/auth.service';
import {EventBusEmitService} from '@shared/service/event-bus-emit.service';
import {CartService} from '@services/cart.service';
import {Cart} from '@modals/cart.modal';
import {RouteConfig} from '@shared/config/route-config';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.scss']
})
export class CartComponent implements OnInit {

  RouteConfig = RouteConfig;
  isLoading = true;
  cart: Cart;


  constructor(private eventBus: EventBusService, private authService: AuthService, private eventBusEmitService: EventBusEmitService, private cartService: CartService) {
  }

  async ngOnInit(): Promise<void> {
    await this.loadCart();
  }

  async loadCart(): Promise<void> {
    try {
      this.eventBusEmitService.showLoading();
      const res = await this.cartService.getAllProductFromCart();
      this.cart = res.data;
      this.isLoading = false;
      this.eventBusEmitService.hideMiniCartModal();
    } catch (e) {
      console.error('::::AAA::::BBB::::', e);
    } finally {
      this.eventBusEmitService.hideLoading();
    }
  }

  async removeProductFromCart(cartItemId): Promise<void> {
    try {
      this.eventBusEmitService.showLoading();
      const res = await this.cartService.removeCartProduct(cartItemId);
      if (res) {
        await this.loadCart();
      }
      if (res.error) {
        this.eventBusEmitService.showMessage({msg: res.error.message});
      } else {
        this.eventBusEmitService.showMessage({msg: res.message});
      }
    } catch (e) {
      console.error('::::AAA::::BBB::::', e);
    } finally {
      this.eventBusEmitService.hideLoading();
    }
  }

  async updateQuantity(cartItemIndex, updateQuantity): Promise<void> {
    try {
      this.eventBusEmitService.showLoading();
      const item = this.cart.items[cartItemIndex];
      const data = {qty: {}};
      data.qty[item.id] = item.quantity + updateQuantity;
      const res = await this.cartService.updateCartProductQty(data);
      if (res) {
        await this.loadCart();
      }
      if (res.error) {
        this.eventBusEmitService.showMessage({msg: res.error.message});
      } else {
        this.eventBusEmitService.showMessage({msg: res.message});
      }
    } catch (e) {
      this.eventBusEmitService.showMessage({msg: e.error.message});
      console.error('::::AAA::::BBB::::', e);
    } finally {
      this.eventBusEmitService.hideLoading();
    }

  }

  async handleClearCartClick(): Promise<void> {
    try {
      const res = await this.cartService.emptyCart();
    } catch (e) {
      console.error('::::AAA::::BBB::::', e);
    }
  }
}
