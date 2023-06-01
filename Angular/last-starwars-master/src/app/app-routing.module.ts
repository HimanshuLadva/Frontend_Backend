import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DetalingComponent } from './detaling/detaling.component';
import { HomeComponent } from './home/home.component';
import { ListingComponent } from './listing/listing.component';

const routes: Routes = [
   {
     path:'',
     component:HomeComponent
   },
   {
    path:'listof/:slug',
    component:ListingComponent
   },
   {
     path:'listof/:slug/:id',
     component:DetalingComponent
   }
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { useHash: true })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
