import { CartComponent } from './components/cart/cart.component';
import { WishlistComponent } from './components/wishlist/wishlist.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { FoodComponent } from './components/food/food.component';
import { FoodinfoComponent } from './components/foodinfo/foodinfo.component';

const routes: Routes = [
  {
    path:'',
    component:HomeComponent,
  },
  {
    path:'wishlist',
    component:WishlistComponent
  },
  {
    path:'cart',
    component:CartComponent
  },
  {
    path:'menu/:slug',
    component:FoodComponent,  
  },
  {
    path:'menu/:slug/:id',
    component:FoodinfoComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { useHash: true })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
