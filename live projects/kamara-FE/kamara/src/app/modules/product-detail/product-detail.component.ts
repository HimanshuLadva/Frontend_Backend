import { Component, OnInit } from '@angular/core';
import { JsLoader } from '@shared/static/js-loader';
import { SubSink } from 'subsink';
import {
  Product,
  productSliderType,
  SelectedSuperAttribute,
  SuperAttribute,
} from '@modals/product.modal';
import { EventBusService } from '@shared/service/event-bus/event-bus.service';
import { ProductService } from '@services/product.service';
import { CartService } from '@services/cart.service';
import { $e } from 'codelyzer/angular/styles/chars';
import { CommonService } from '@services/common.service';
import { EventBusEmitService } from '@shared/service/event-bus-emit.service';
import { Static } from '@shared/static/static';
import { environment } from '../../../environments/environment';
import {
  ActivatedRoute,
  NavigationEnd,
  NavigationError,
  NavigationStart,
  Router,
} from '@angular/router';
import { AuthService } from '@auth/service/auth.service';
import { RouteConfig } from '@shared/config/route-config';

@Component({
  selector: 'app-product-detail',
  templateUrl: './product-detail.component.html',
  styleUrls: ['./product-detail.component.scss'],
})
export class ProductDetailComponent implements OnInit {
  subs = new SubSink();
  isLoading = false;
  product: Product;
  currentProduct: Product;
  selectedAttributed: SelectedSuperAttribute[] = [];
  attributes: SuperAttribute[] = [];
  productSliderType = productSliderType;
  quantity = 1;
  slug;
  constructor(
    private eventBus: EventBusService,
    private productService: ProductService,
    private cartService: CartService,
    private eventBusEmitService: EventBusEmitService,
    private route: ActivatedRoute,
    public auth: AuthService,
    private router: Router
  ) {}

  get currentProductForAttribute(): Product | null {
    return this.product?.variants?.find((x) => x.id == this.currentProduct?.id);
  }

  ngOnInit(): void {
    this.slug = this.route.snapshot.paramMap.get('slug');
    if (this.slug) {
      this.loadProduct(this.slug);
    }
    this.router.events.subscribe((event: any) => {
      if (event instanceof NavigationEnd) {
        this.slug = this.route.snapshot.paramMap.get('slug');
        if (
          this.slug &&
          event?.url?.toLowerCase().includes(RouteConfig.productDetail)
        ) {
          this.loadProduct(this.slug);
        }
      }
    });
  }

  async loadProduct(ProductSlug): Promise<void> {
    try {
      this.eventBusEmitService.showLoading();
      this.isLoading = true;
      if (this.currentProduct?.images) {
        this.currentProduct.images = null;
      }
      this.product = null;
      const res = await this.productService.getProductBySlugForProductDetail(
        ProductSlug
      );
      this.product = res.data;
      this.attributes = [];
      if (this.product.super_attributes?.length > 0) {
        this.loadAttribute();
        await this.loadSelectedProduct(this.product.variants[0].id);
      } else {
        this.currentProduct = res.data;
      }
    } catch (e) {
      console.error('::::AAA::::BBB::::', e);
    } finally {
      this.eventBusEmitService.hideLoading();
      this.isLoading = false;
    }
  }

  loadVariation(): void {
    console.clear();
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
    if (
      this.product.variants[selectedVariantIndex].id !== this.currentProduct.id
    ) {
      this.loadSelectedProduct(this.product.variants[selectedVariantIndex].id);
      this.quantity = 1;
    }
  }

  handleOnAttributeChange($event: SelectedSuperAttribute[]): void {
    this.selectedAttributed = $event;
    this.loadVariation();
  }

  loadAttribute(): void {
    let superAttribute: SuperAttribute;
    this.product.super_attributes?.forEach((superAttr) => {
      superAttribute = { ...superAttr, options: [] };
      superAttribute.options = superAttr.options?.filter(({ id }) => {
        return this.product.variants?.find(
          (variant) => variant[superAttr.code] == id
        );
      });
      this.attributes.push(superAttribute);
    });
  }

  updateQuantity(increment): void {
    this.quantity += increment;
  }

  async loadSelectedProduct(productId): Promise<void> {
    try {
      this.eventBusEmitService.showLoading();
      this.isLoading = true;
      if (this.currentProduct?.images) {
        this.currentProduct.images = null;
      }
      const res = await this.productService.getProductByIdForQuickView(
        productId
      );
      this.currentProduct = res.data;
    } catch (e) {
      console.error('::::AAA::::BBB::::', e);
    } finally {
      this.eventBusEmitService.hideLoading();
      this.isLoading = false;
    }
  }
  async addToCart(): Promise<void> {
    try {
      const res = await this.cartService.addProduct(
        this.currentProduct.id,
        CommonService.getProductDataForAddToCart(
          this.currentProduct,
          this.quantity
        )
      );
      if (res.error) {
        this.eventBusEmitService.showMessage({ msg: res.error.message });
      } else {
        this.eventBusEmitService.showMessage({ msg: res.message });
      }
    } catch (e) {
      console.error('::::AAA::::BBB::::', e);
    }
  }

  async getMoreDetail(): Promise<void> {
    try {
      const res = await this.productService.getProductDetail(
        this.currentProduct.id
      );
    } catch (e) {
      console.error('::::AAA::::BBB::::', e);
    }
  }
}
