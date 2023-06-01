import { AboutMeComponent } from './about-me/about-me.component';
import { AboutCompanyComponent } from './about-company/about-company.component';
import { ErrorMsgComponent } from './error-msg/error-msg.component';
import { AboutComponent } from './about/about.component';
import { GalleryComponent } from './gallery/gallery.component';
import { ContactComponent } from './contact/contact.component';
import { HomeComponent } from './home/home.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    component:AboutComponent,
    path:'about',
    children: [
      {path:'company',component: AboutCompanyComponent },
      {path:'me', component: AboutMeComponent},
    ]
  },
  {
    component:HomeComponent,
    path:'home',
  },
  {
    component:ContactComponent,
    path:'contact/:id',
  },
  {
    component:ContactComponent,
    path:'contact',
  },
  {
    component:GalleryComponent,
    path:'gallery',
  },
  {
    component:ErrorMsgComponent,
    path:'**',
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
