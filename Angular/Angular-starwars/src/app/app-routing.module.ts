// import { ChardetailComponent } from './chardetail/chardetail.component';
import { CharlistComponent } from './charlist/charlist.component';

import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  // {path:'charlist', component:CharlistComponent},
  // {path:'charlist/chardetail', component:ChardetailComponent},
  {path:'', component:CharlistComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
