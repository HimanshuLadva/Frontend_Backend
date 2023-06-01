import { LoginComponent } from './login/login.component';
import { ListComponent } from './list/list.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

console.log("hello lazy loading!!!!!");

const routes: Routes = [
  { path:'login', component:LoginComponent},
  { path:'list', component:ListComponent}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminRoutingModule { }
