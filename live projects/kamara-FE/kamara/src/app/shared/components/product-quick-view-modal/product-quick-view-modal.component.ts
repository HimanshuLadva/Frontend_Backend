import { Component, OnDestroy, OnInit } from '@angular/core';
import { EventBusService } from '../../service/event-bus/event-bus.service';
import { BusEvents } from '../../service/event-bus/bus-events';
import { SubSink } from 'subsink';
import { ProductService } from '@services/product.service';
import { JsLoader } from '@shared/static/js-loader';
import { CartService } from '@services/cart.service';
import { CommonService } from '@services/common.service';
import { Product } from '@modals/product.modal';

declare var $;

@Component({
  selector: 'app-product-quick-view-modal',
  templateUrl: './product-quick-view-modal.component.html',
  styleUrls: ['./product-quick-view-modal.component.scss'],
})
export class ProductQuickViewModalComponent implements OnInit, OnDestroy {
  subs = new SubSink();
  product: Product;
  selectedAttributed: { code?: string; value?: any }[] = [];

  constructor(
    private eventBus: EventBusService,
    private productService: ProductService,
    private cartService: CartService
  ) {}

  async ngOnInit(): Promise<void> {
    await this.listenEventBus();
    // this.loadProduct(38).then(() => {
    //   if (this.product) {
    //     $('#quick_view_modal').modal('show');
    //     setTimeout(() => {
    //       JsLoader.loadProductQuickViewJs();
    //     }, 1000);
    //   }
    // });
  }

  async listenEventBus(): Promise<void> {
    this.subs.sink = this.eventBus.on(BusEvents.showQuickViewModal, (id) => {
      this.loadProduct(id).then(() => {
        if (this.product) {
          $('#quick_view_modal').modal('show');
          setTimeout(() => {
            JsLoader.loadProductQuickViewJs();
          }, 1000);
        }
      });
    });
  }

  async loadProduct(productId): Promise<void> {
    try {
      this.product = null;
      const res = await this.productService.getProductByIdForQuickView(
        productId
      );
      this.product = res.data;
      setTimeout(() => {
      }, 1000);
    } catch (e) {
      console.error('::::AAA::::BBB::::', e);
    }
  }

  async addToCart(): Promise<void> {
    try {
      const res = await this.cartService.addProduct(
        this.product.id,
        CommonService.getProductDataForAddToCart(this.product)
      );
    } catch (e) {
      console.error('::::AAA::::BBB::::', e);
    }
  }

  ngOnDestroy(): void {
    this.subs.unsubscribe();
  }

  handleAttributeChange($event: any): void {
    const index = this.selectedAttributed?.findIndex(
      (d) => d.code == $event.code
    );
    if (index > -1) {
      this.selectedAttributed[index].value = $event.value;
    } else {
      if (this.selectedAttributed?.length > 0) {
        this.selectedAttributed.push($event);
      } else {
        this.selectedAttributed = [$event];
      }
    }
    if (this.selectedAttributed?.length > 0) {
      this.loadVariation();
    }
  }

  loadVariation(): void {
    let l = -1;
    let selectedVariantIndex = -1;
    this.product.variants?.forEach((variant, index) => {
      let count = 0;
      this.selectedAttributed?.forEach((d) => {
        if (variant[d.code] == d.value) {
          count++;
        }
      });
      if (count > l) {
        l = count;
        selectedVariantIndex = index;
      }
    });
  }
}
