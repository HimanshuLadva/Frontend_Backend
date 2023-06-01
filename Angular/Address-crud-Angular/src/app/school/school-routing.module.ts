import { EditClassComponent } from './edit-class/edit-class.component';
import { AddClassComponent } from './add-class/add-class.component';
import { ViewClassComponent } from './view-class/view-class.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {path: 'class/view', component:ViewClassComponent},
  {path: 'class/add', component:AddClassComponent},
  {path: 'class/edit/:id', component: EditClassComponent}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SchoolRoutingModule { }
