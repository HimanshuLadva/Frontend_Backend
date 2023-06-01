import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http';
import { CharlistComponent } from './charlist/charlist.component';
import { ChardetailComponent } from './chardetail/chardetail.component';

@NgModule({
  declarations: [
    AppComponent,
    CharlistComponent,
    ChardetailComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }