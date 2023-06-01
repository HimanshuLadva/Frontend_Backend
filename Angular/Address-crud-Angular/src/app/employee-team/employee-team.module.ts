import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EmployeeTeamRoutingModule } from './employee-team-routing.module';
import { AddTeamComponent } from './add-team/add-team.component';
import { ViewTeamComponent } from './view-team/view-team.component';
import { EditTeamComponent } from './edit-team/edit-team.component';
import { FormsModule ,ReactiveFormsModule } from '@angular/forms';


@NgModule({
  declarations: [
    AddTeamComponent,
    ViewTeamComponent,
    EditTeamComponent
  ],
  imports: [
    CommonModule,
    EmployeeTeamRoutingModule,
    FormsModule,
    ReactiveFormsModule
  ]
})
export class EmployeeTeamModule { }
