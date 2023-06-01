import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { ErrorInterceptor } from '@auth/halpers/error.interceptor';
import { JwtInterceptor } from '@auth/halpers/jwt.interceptor';
import { CartComponent } from '@components/cart/cart.component';
import { HomeModule } from '@modules/home/home.module';
import { ProductListComponent } from '@modules/product-list/product-list.component';
import { SearchProductListComponent } from '@modules/search-product-list/search-product-list.component';
import { WishlistComponent } from '@modules/wishlist/wishlist.component';
import { FilterAttributeComponent } from '@pages/filter-attribute/filter-attribute.component';
import { FilterComponent } from '@pages/filter/filter.component';
import { DynamicScriptLoaderService } from '@shared/service/dynamic-script-loader.service';
import { SharedModule } from '@shared/shared.module';
// import { NiceSelectModule } from 'ng-nice-select';
import { InfiniteScrollModule } from 'ngx-infinite-scroll';
import { NgxPaginationModule } from 'ngx-pagination';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CoreModule } from './core/core.module';
import { CheckoutComponent } from './modules/checkout/checkout.component';
import { OrderDetailComponent } from './modules/order-detail/order-detail.component';
import { ProductDetailComponent } from './modules/product-detail/product-detail.component';
import { CheckoutAddressComponent } from './pages/checkout-address/checkout-address.component';
import { CheckoutBillingFormComponent } from './pages/checkout-billing-form/checkout-billing-form.component';
import { CheckoutFormComponent } from './pages/checkout-form/checkout-form.component';
import { ProductDetailVariationAttributeListComponent } from './pages/product-detail-variation-attribute-list/product-detail-variation-attribute-list.component';
import { BlogDetailComponent } from './modules/blog-detail/blog-detail.component';
import { NoProductFoundComponent } from './modules/no-product-found/no-product-found.component';
import { AboutUsComponent } from './modules/about-us/about-us.component';
import { ContactUsComponent } from './modules/contact-us/contact-us.component';
import { PrivacyPolicyComponent } from './modules/privacy-policy/privacy-policy.component';

@NgModule({
  declarations: [
    AppComponent,
    CartComponent,
    WishlistComponent,
    ProductListComponent,
    FilterComponent,
    FilterAttributeComponent,
    ProductDetailComponent,
    SearchProductListComponent,
    CheckoutComponent,
    CheckoutFormComponent,
    CheckoutAddressComponent,
    CheckoutBillingFormComponent,
    OrderDetailComponent,
    ProductDetailVariationAttributeListComponent,
    BlogDetailComponent,
    NoProductFoundComponent,
    AboutUsComponent,
    ContactUsComponent,
    PrivacyPolicyComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'serverApp' }),
    AppRoutingModule,
    CoreModule,
    HttpClientModule,
    NgxPaginationModule,
    // NiceSelectModule,
    SharedModule,
    ReactiveFormsModule,
    HomeModule,
    InfiniteScrollModule,
  ],
  providers: [
    DynamicScriptLoaderService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: ErrorInterceptor,
      multi: true,
    },
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
