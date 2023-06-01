import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { OneSliderProductComponent } from './components/one-slider-product/one-slider-product.component';
import { ProductQuickViewModalComponent } from './components/product-quick-view-modal/product-quick-view-modal.component';
import { BreadcrumbComponent } from './components/breadcrumb/breadcrumb.component';
import { ProductListOneProductComponent } from './components/product-list-one-product/product-list-one-product.component';
import { PriceRangeComponent } from './components/price-range/price-range.component';
import { NgxSliderModule } from '@angular-slider/ngx-slider';
import { PaginationComponent } from './components/pagination/pagination.component';
import { NgxPaginationModule } from 'ngx-pagination';
import { TrimPipe } from './pipes/trim.pipe';
import { RouterModule } from '@angular/router';
import { SuperAttributeComponent } from './components/super-attribute/super-attribute.component';
// import { NiceSelectModule } from 'ng-nice-select';

import { AutocompleteComponent } from './components/autocomplete/autocomplete.component';
import { AutocompleteLibModule } from 'angular-ng-autocomplete';
import { FormsModule } from '@angular/forms';
import { AddressComponent } from './components/address/address.component';
import { CollapseComponent } from './components/collapse/collapse.component';
import { ProductDetailPolicySliderComponent } from './components/product-detail-policy-slider/product-detail-policy-slider.component';
import { ProductDetailImageComponent } from './components/product-detail-image/product-detail-image.component';
import { RatingStarsComponent } from './components/rating-stars/rating-stars.component';
import { MobileFilterComponent } from './components/mobile-filter/mobile-filter.component';

@NgModule({
  declarations: [
    OneSliderProductComponent,
    ProductQuickViewModalComponent,
    BreadcrumbComponent,
    ProductListOneProductComponent,
    PriceRangeComponent,
    PaginationComponent,
    TrimPipe,
    SuperAttributeComponent,
    AddressComponent,
    AutocompleteComponent,
    CollapseComponent,
    ProductDetailPolicySliderComponent,
    ProductDetailImageComponent,
    RatingStarsComponent,
    MobileFilterComponent,
  ],
  exports: [
    OneSliderProductComponent,
    ProductQuickViewModalComponent,
    BreadcrumbComponent,
    ProductListOneProductComponent,
    PriceRangeComponent,
    PaginationComponent,
    TrimPipe,
    AutocompleteComponent,
    AddressComponent,
    CollapseComponent,
    ProductDetailPolicySliderComponent,
    ProductDetailImageComponent,
    RatingStarsComponent,
    MobileFilterComponent,
  ],
  imports: [
    CommonModule,
    NgxSliderModule,
    NgxPaginationModule,
    RouterModule,
    // NiceSelectModule,
    AutocompleteLibModule,
    FormsModule,
  ],
})
export class SharedModule {}
