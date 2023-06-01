import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from './app.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatSliderModule } from '@angular/material/slider';
import { MatButtonModule} from '@angular/material/button';
import { ChildComponent } from './child/child.component';
import { InnerChildComponent } from './child/inner-child/inner-child.component';
import { UserDetailsComponent } from './user-details/user-details.component';
import { UserDetails2Component } from './user-details2/user-details2.component';
import { UsdInrPipe } from './pipes/usd-inr.pipe';
import { FormsModule } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { RedElDirective } from './red-el.directive';
import { ChangeElDirective } from './change-el.directive'

@NgModule({
  declarations: [ 
    AppComponent, ChildComponent, InnerChildComponent, UserDetailsComponent, UserDetails2Component, UsdInrPipe, RedElDirective, ChangeElDirective
  ],
  imports: [
    BrowserModule,
    FormsModule,
    NgbModule,
    BrowserAnimationsModule,
    MatSliderModule,
    MatButtonModule,
    ReactiveFormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
