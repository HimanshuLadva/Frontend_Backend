import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PeopledetailComponent } from './peopledetail/peopledetail.component';
import { PeoplelistComponent } from './peoplelist/peoplelist.component';

const routes: Routes = [
  {path:"", component:PeoplelistComponent},
  {path:':id', component:PeopledetailComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
