import { Component, OnInit, ViewChild } from '@angular/core';
import { EventBusEmitService } from '@shared/service/event-bus-emit.service';
import { CartService } from '@services/cart.service';
import { Cart } from '@modals/cart.modal';
import { CheckoutAddressComponent } from '@pages/checkout-address/checkout-address.component';
import { CheckoutService } from '@services/checkout.service';
import { RouteConfig } from '@shared/config/route-config';
import { Router } from '@angular/router';

@Component({
  selector: 'app-checkout',
  templateUrl: './checkout.component.html',
  styleUrls: ['./checkout.component.scss'],
})
export class CheckoutComponent implements OnInit {
  @ViewChild('checkoutAddressComponent')
  checkoutAddressComponent: CheckoutAddressComponent;

  RouteConfig = RouteConfig;

  isLoading = true;
  cart: Cart;
  selectedAddress;

  constructor(
    private eventBusEmitService: EventBusEmitService,
    private cartService: CartService,
    private checkoutService: CheckoutService,
    private router: Router
  ) {}

  async ngOnInit(): Promise<void> {
    this.eventBusEmitService.hideMiniCartModal();
    await this.loadCart();
  }

  async handleSaveOrder(): Promise<void> {
    const res = this.checkoutAddressComponent.getAddressData();
    if (!res.isValid) {
      return;
    }
    this.selectedAddress = res.address;
    await this.placeOrder();
  }

  async placeOrder(): Promise<void> {
    try {
      this.eventBusEmitService.showLoading();
      let res = await this.checkoutService.saveAddressToCard(
        this.selectedAddress
      );
      if (res && !res.error) {
        res = await this.checkoutService.saveShippingMethodToCard();
        if (res && !res.error) {
          res = await this.checkoutService.savePaymentMethodToCard();
          if (res && !res.error) {
            res = await this.checkoutService.saveOrder();
            if (res?.success && res?.order?.id) {
              this.router
                .navigate([RouteConfig.orderDetail, res?.order?.id])
                .then(() => {
                  this.eventBusEmitService.reloadCartCount();
                  this.eventBusEmitService.hideLoading();
                  this.eventBusEmitService.showMessage({
                    msg: 'Your Order Placed Successfully',
                  });
                });
            }
          }
        }
      }
    } catch (e) {
      console.error('::::AAA::::BBB::::', e);
    } finally {
      this.eventBusEmitService.hideLoading();
    }
  }

  private async loadCart(): Promise<void> {
    try {
      this.isLoading = true;
      const res = await this.cartService.getCartDetailForCheckout();
      console.clear();
      this.cart = res.data;
      this.isLoading = false;
    } catch (e) {
      console.error('::::AAA::::BBB::::', e);
    }
  }
}
