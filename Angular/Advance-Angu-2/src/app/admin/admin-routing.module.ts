import { ListComponent } from './../admin/list/list.component';
import { LoginComponent } from './../admin/login/login.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: 'admin',
    children: [
      { path: 'login', component: LoginComponent },
      { path: 'list', component: ListComponent },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class AdminRoutingModule {}
