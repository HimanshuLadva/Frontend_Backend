import { Component, OnDestroy, OnInit } from '@angular/core';
import { JsLoader } from '../../../shared/static/js-loader';
import { RouteConfig } from '@shared/config/route-config';
import { AuthService } from '@auth/service/auth.service';
import { EventBusEmitService } from '@shared/service/event-bus-emit.service';
import { EventBusService } from '@shared/service/event-bus/event-bus.service';
import { SubSink } from 'subsink';
import { BusEvents } from '@shared/service/event-bus/bus-events';
import { WishlistService } from '@services/wishlist.service';
import { CartService } from '@services/cart.service';
import { CategoryTree } from '@modals/category-tree.modal';
import { MegaMenuService } from '../../services/mega-menu.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss'],
})
export class HeaderComponent implements OnInit, OnDestroy {
  RouteConfig = RouteConfig;
  subs = new SubSink();
  wishlistCount = 0;
  cartCount = 0;
  categoryTree: CategoryTree[];
  hideMessage = false;

  constructor(
    private megaMenuService: MegaMenuService,
    public eventBusEmitService: EventBusEmitService,
    public authService: AuthService,
    private eventBus: EventBusService,
    private wishlistService: WishlistService,
    private cartService: CartService,
    private auth: AuthService
  ) {}

  async ngOnInit(): Promise<void> {
    this.hideMessage = this.auth.hideMessage;
    if (this.authService.isUserLogin) {
      this.reloadWishlistCount();
      this.reloadCartCount();
    }
    const res = await this.megaMenuService.getCategoryTree();
    this.categoryTree = res.data;
    this.listenEventBus();
    setTimeout(() => {
      JsLoader.loadStickyMenuJs();
      JsLoader.loadTooltip();
    }, 500);
  }

  async listenEventBus(): Promise<void> {
    this.subs.sink = this.eventBus.on(BusEvents.reloadWishlistCount, () => {
      this.reloadWishlistCount();
    });
    this.subs.sink = this.eventBus.on(BusEvents.reloadCartCount, () => {
      this.reloadCartCount();
    });
  }

  ngOnDestroy(): void {
    this.subs.unsubscribe();
  }

  private async reloadWishlistCount(): Promise<void> {
    try {
      this.wishlistCount =
        await this.wishlistService.getCustomerWishlistProductCount(
          this.authService.User.id
        );
    } catch (e) {
      console.error('::::AAA::::BBB::::', e);
    }
  }

  private async reloadCartCount(): Promise<void> {
    try {
      this.cartCount = await this.cartService.getAllProductCountFromCart();
    } catch (e) {
      console.error('::::AAA::::BBB::::', e);
    }
  }
  removeWelcomeMessage(): any {
    this.hideMessage = true;
    this.auth.setMessage();
  }
}
