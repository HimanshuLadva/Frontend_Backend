import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CoreRoutingModule } from './core-routing.module';
import { CoreComponent } from './core.component';
import { HeaderComponent } from './components/header/header.component';
import { FooterComponent } from './components/footer/footer.component';
import {HomeModule} from '../modules/home/home.module';
import { MegaMenuComponent } from './components/mega-menu/mega-menu.component';
import {SharedModule} from '../shared/shared.module';
import { MiniCartComponent } from './components/mini-cart/mini-cart.component';
import { NotFoundComponent } from './components/not-found/not-found.component';


@NgModule({
  declarations: [
    CoreComponent,
    HeaderComponent,
    FooterComponent,
    MegaMenuComponent,
    MiniCartComponent,
    NotFoundComponent,
  ],
  exports: [CoreComponent, NotFoundComponent],
  imports: [CommonModule, CoreRoutingModule, HomeModule, SharedModule],
})
export class CoreModule {}
