import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AddressModule } from './address/address.module';
import { EmployeeTeamModule } from './employee-team/employee-team.module';
import { SchoolModule } from './school/school.module';

@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    AddressModule,
    EmployeeTeamModule,
    SchoolModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
