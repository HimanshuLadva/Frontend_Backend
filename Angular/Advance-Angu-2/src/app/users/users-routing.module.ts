import { ListComponent } from './../users/list/list.component';
import { LoginComponent } from './../users/login/login.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: 'users',
    children: [
      { path: 'login', component: LoginComponent },
      { path: 'list', component: ListComponent },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UsersRoutingModule { }
