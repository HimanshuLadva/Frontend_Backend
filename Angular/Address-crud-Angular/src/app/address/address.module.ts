import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AddressRoutingModule } from './address-routing.module';
import { AddressListComponent } from './address-list/address-list.component';
import { AddAddressComponent } from './add-address/add-address.component';
import { FormsModule ,ReactiveFormsModule } from '@angular/forms';
import { EditAddressComponent } from './edit-address/edit-address.component';


@NgModule({
  declarations: [
    AddressListComponent,
    AddAddressComponent,
    EditAddressComponent
  ],
  imports: [
    CommonModule,
    AddressRoutingModule,
    FormsModule,
    ReactiveFormsModule
  ]
})
export class AddressModule { }
