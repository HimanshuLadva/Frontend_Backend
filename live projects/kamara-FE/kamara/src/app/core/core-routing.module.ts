import { PrivacyPolicyComponent } from './../modules/privacy-policy/privacy-policy.component';
import { PriceRangeComponent } from './../shared/components/price-range/price-range.component';
import { ContactUsComponent } from './../modules/contact-us/contact-us.component';
import { AboutUsComponent } from './../modules/about-us/about-us.component';
import { BlogDetailComponent } from '@modules/blog-detail/blog-detail.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from '@modules/home/home.component';
import { CartComponent } from '@components/cart/cart.component';
import { WishlistComponent } from '@modules/wishlist/wishlist.component';
import { AuthGuard } from '@auth/halpers/auth-guard.service';
import { ProductListComponent } from '@modules/product-list/product-list.component';
import { SearchProductListComponent } from '@modules/search-product-list/search-product-list.component';
import { CheckoutComponent } from '@modules/checkout/checkout.component';
import { OrderDetailComponent } from '@modules/order-detail/order-detail.component';
import { NotFoundComponent } from './components/not-found/not-found.component';
import { ProductDetailComponent } from '@modules/product-detail/product-detail.component';
import { NoProductFoundComponent } from '@modules/no-product-found/no-product-found.component';  

const routes: Routes = [
  {
    path: '',
    component: HomeComponent,
  },
  {
    path:'category/about-us',
    component:AboutUsComponent
  },
  {
    path:'category/contact-us',
    component:ContactUsComponent
  },
  {
    path: 'category/:slug',
    component: ProductListComponent,
  },
  {
    path: 'category',
    component: ProductListComponent,
  },
  {
    path: 'product/:slug',
    component: ProductDetailComponent,
  },
  {
    path: 'product',
    component: ProductDetailComponent,
  },
  {
    path: 'blog-detail/:id',
    component:BlogDetailComponent,
  },
  {
    path: 'search/:keyword',
    component: SearchProductListComponent,
  },
  {
    path: 'search',
    component: SearchProductListComponent,
  },
  {
    path: 'cart',
    component: CartComponent,
    canActivate: [AuthGuard],
  },
  {
    path: 'checkout',
    component: CheckoutComponent,
    canActivate: [AuthGuard],
  },
  {
    path:'privacy-policy',
    children:[
      {
        path:"policy",
        component:PrivacyPolicyComponent
      },
      {
        path:"terms&condition",
        component:PrivacyPolicyComponent
      },
      {
        path:"shopping-policy",
        component:PrivacyPolicyComponent
      }, 
      {
        path:"cancellation&refund-policy",
        component:PrivacyPolicyComponent
      }
    ],
    // component:PrivacyPolicyComponent,
  },
  {
    path: 'wishlist',
    component: WishlistComponent,
    canActivate: [AuthGuard]
  },
  {
    path: 'order-detail/:orderId',
    component: OrderDetailComponent,
    canActivate: [AuthGuard],
  },
  {
    path: 'auth',
    loadChildren: () => import('@auth/auth.module').then((m) => m.AuthModule),
  },
  {
     path: 'not-found',
     component: NoProductFoundComponent,
  },
  {
    // path: '**', redirect To: ''
    path: '**',
    component: NotFoundComponent,
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class CoreRoutingModule {}
