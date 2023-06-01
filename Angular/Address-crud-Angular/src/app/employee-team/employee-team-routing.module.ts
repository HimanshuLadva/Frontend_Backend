import { ViewTeamComponent } from './view-team/view-team.component';
import { EditTeamComponent } from './edit-team/edit-team.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddTeamComponent } from './add-team/add-team.component';

const routes: Routes = [
  {path:'teamemployee/add',component:AddTeamComponent},
  {path:'teamemployee/view', component:ViewTeamComponent},
  {path:'teamemployee/edit/:id',component:EditTeamComponent},
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class EmployeeTeamRoutingModule { }
