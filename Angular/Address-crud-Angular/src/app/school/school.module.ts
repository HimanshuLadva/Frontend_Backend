import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SchoolRoutingModule } from './school-routing.module';
import { AddClassComponent } from './add-class/add-class.component';
import { EditClassComponent } from './edit-class/edit-class.component';
import { ViewClassComponent } from './view-class/view-class.component';
import { FormsModule ,ReactiveFormsModule } from '@angular/forms';


@NgModule({
  declarations: [
    AddClassComponent,
    EditClassComponent,
    ViewClassComponent,
  ],
  imports: [
    CommonModule,
    SchoolRoutingModule,
    FormsModule,
    ReactiveFormsModule
  ]
})
export class SchoolModule { }
