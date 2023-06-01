import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { Product } from '../../modals/product-slider.modal';
import { ProductService } from '../../services/product.service';
import { CategoryService } from '../../shared/service/category.service';
import { JsLoader } from '../../shared/static/js-loader';
import { Category } from '../../modals/category.modal';
import { EventBusEmitService } from '@shared/service/event-bus-emit.service';
import { productSliderType } from '@modals/product.modal';

declare var $;

let key = '';

@Component({
  selector: 'app-product-slider',
  templateUrl: './product-slider.component.html',
  styleUrls: ['./product-slider.component.scss'],
})
export class ProductSliderComponent implements OnInit {
  ROOT_CATEGORY_ID = 1;

  @Input() title: string;
  @Input() subTitle: string;
  @Input() type: productSliderType;
  @Input() hideCategoryFilter = false;
  @Input() productId = 0;

  @ViewChild('divRef') ref;

  loading = true;
  smallLoading = false;
  ProductList: Product[];
  CategoryList: Category[];
  selectedCategoryId = this.ROOT_CATEGORY_ID;

  defaultParameter: { key: string; value: any }[] = [
    { key: 'hide', value: 'categories,description,images,variants' },
    { key: 'limit', value: 6 },
  ];

  // product: Product;

  constructor(
    private productService: ProductService,
    private categoryService: CategoryService,
    private eventBusEmitService: EventBusEmitService
  ) {}

  async ngOnInit(): Promise<void> {
    if (this.type === productSliderType.featured) {
      this.defaultParameter.push({ key: 'featured', value: 1 });
    } else if (this.type === productSliderType.best_seller) {
      this.defaultParameter.push({ key: 'new', value: 1 });
      // this.defaultParameter.push({key: 'category_id', value: 4});
    }
    await this.loadDefaultData();
  }

  async loadDefaultData(): Promise<void> {
    try {
      if (!this.hideCategoryFilter) {
        this.loadCategoryList();
      }
      await this.loadProductList();
      this.loading = false;
      setTimeout(() => {
        if (this.hideCategoryFilter) {
          JsLoader.loadProductDetailSliderJs(this.ref?.nativeElement);
        } else {
          JsLoader.loadProductSliderJs(this.ref?.nativeElement);
        }
      }, 100);
    } catch (e) {
      console.error('::::AAA::::BBB::::', e);
    }
  }

  async loadProductList(data = null): Promise<void> {
    if (this.type === productSliderType.similar_design_products) {
      await this.loadProductDetailSimilarProducts();
      return;
    }
    if (this.type === productSliderType.complete_the_look_products) {
      await this.loadProductDetailCompleteTheLook();
      return;
    }
    try {
      if (data == null) {
        data = { abc: 1 };
      }
      this.defaultParameter.forEach((x) => {
        data[x.key] = x.value;
      });
      if (!data?.category_id) {
        data.category_id = this.ROOT_CATEGORY_ID;
      }
      const res = await this.productService.getFeaturedProductListing(data);
      this.ProductList = res.data;
    } catch (e) {
      console.error('::::AAA::::BBB::::', e);
    }
  }

  async loadProductDetailSimilarProducts(): Promise<void> {
    try {
      const res = await this.productService.getProductSimilarDesign(
        this.productId
      );
      this.ProductList = res.data;
    } catch (e) {
      console.error('::::AAA::::BBB::::', e);
    }
  }

  async loadProductDetailCompleteTheLook(): Promise<void> {
    try {
      const res = await this.productService.getProductCompleteTheLook(
        this.productId
      );
      this.ProductList = res.data;
    } catch (e) {
      console.error('::::AAA::::BBB::::', e);
    }
  }

  async loadCategoryList(data = null): Promise<void> {
    try {
      if (data == null) {
        data = { parent_id: 1, source: 'homepage' };
      }
      const res = await this.categoryService.getAllDescendantCategories(data);
      res.data.pop();
      res.data.pop();
      this.CategoryList = res.data;
    } catch (e) {
      console.error('::::AAA::::BBB::::', e);
    }
  }

  async loadCategoryWiseProduct(categoryId: number): Promise<void> {
    try {
      this.eventBusEmitService.showLoading();
      this.smallLoading = true;
      let data;
      if (categoryId != -1) {
        data = { category_id: categoryId };
        this.selectedCategoryId = categoryId;
      }
      await this.loadProductList(data);
      this.smallLoading = false;
      setTimeout(() => {
        JsLoader.loadProductSliderJs(this.ref.nativeElement);
      }, 100);
    } catch (e) {
      console.error('::::AAA::::BBB::::', e);
    } finally {
      this.eventBusEmitService.hideLoading();
    }
  }
}
