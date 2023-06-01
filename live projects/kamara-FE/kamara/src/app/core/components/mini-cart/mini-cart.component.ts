import { Component, OnDestroy, OnInit } from '@angular/core';
import { EventBusEmitService } from '@shared/service/event-bus-emit.service';
import { CartService } from '@services/cart.service';
import { AuthService } from '@auth/service/auth.service';
import { Cart } from '@modals/cart.modal';
import { SubSink } from 'subsink';
import { EventBusService } from '@shared/service/event-bus/event-bus.service';
import { BusEvents } from '@shared/service/event-bus/bus-events';
import { RouteConfig } from '@shared/config/route-config';
import { NavigationEnd, Router } from '@angular/router';

declare var $;

@Component({
  selector: 'app-mini-cart',
  templateUrl: './mini-cart.component.html',
  styleUrls: ['./mini-cart.component.scss'],
})
export class MiniCartComponent implements OnInit, OnDestroy {
  RouteConfig = RouteConfig;
  subs = new SubSink();
  isLoading = true;
  cart: Cart;

  constructor(
    private eventBus: EventBusService,
    private authService: AuthService,
    private eventBusEmitService: EventBusEmitService,
    private cartService: CartService,
    private router: Router
  ) {
    this.router.events.subscribe((event: any) => {
      if (event instanceof NavigationEnd) {
        this.hideMiniCart();
      }
    });
  }

  async ngOnInit(): Promise<void> {
    await this.listenEventBus();
  }

  async loadCart(): Promise<void> {
    try {
      this.eventBusEmitService.showLoading();
      const res = await this.cartService.getAllProductFromCart();
      this.cart = res.data;
      this.isLoading = false;
    } catch (e) {
      console.error('::::AAA::::BBB::::', e);
    } finally {
      this.eventBusEmitService.hideLoading();
    }
  }

  async listenEventBus(): Promise<void> {
    this.subs.sink = this.eventBus.on(BusEvents.showMiniCartModal, () => {
      this.loadCart().then(() => {
        this.showMiniCart();
      });
    });
    this.subs.sink = this.eventBus.on(BusEvents.hideMiniCart, () => {
      this.hideMiniCart();
    });
  }

  ngOnDestroy(): void {
    this.subs.unsubscribe();
  }

  showMiniCart(): void {
    $('body').addClass('fix');
    $('.minicart-inner').addClass('show');
  }

  hideMiniCart(): void {
    $('body').removeClass('fix');
    $('.minicart-inner').removeClass('show');
  }

  async removeProductFromCart(cartItemId): Promise<void> {
    try {
      this.eventBusEmitService.showLoading();
      const res = await this.cartService.removeCartProduct(cartItemId);
      if (res) {
        await this.loadCart();
      }
    } catch (e) {
      console.error('::::AAA::::BBB::::', e);
    } finally {
      this.eventBusEmitService.hideLoading();
    }
  }
}
