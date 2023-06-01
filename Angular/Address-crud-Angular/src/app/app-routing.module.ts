import { ViewTeamComponent } from './employee-team/view-team/view-team.component';
import { AddressListComponent } from './address/address-list/address-list.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ViewClassComponent } from './school/view-class/view-class.component';

const routes: Routes = [
   {path:'address', component:AddressListComponent},
   {path:'teamemployee', component:ViewTeamComponent},
   {path:'class', component:ViewClassComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
