import { AddAddressComponent } from './add-address/add-address.component';
import { AddressListComponent } from './address-list/address-list.component';
import { EditAddressComponent } from './edit-address/edit-address.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  // {path: '', redirectTo:'addresslist', pathMatch:"full"},   
  {path: 'addresslist', component:AddressListComponent},
  {path: 'add', component: AddAddressComponent},
  {path: 'edit/:id', component: EditAddressComponent},
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AddressRoutingModule { }
