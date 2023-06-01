import { Component, Input, OnInit } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';
import { AuthService } from '@auth/service/auth.service';
import { CategoryTree } from '@modals/category-tree.modal';
import { RouteConfig } from '@shared/config/route-config';
import { JsLoader } from '@shared/static/js-loader';

import { MegaMenuService } from '../../services/mega-menu.service';

@Component({
  selector: 'app-mega-menu',
  templateUrl: './mega-menu.component.html',
  styleUrls: ['./mega-menu.component.scss'],
})
export class MegaMenuComponent implements OnInit {
  RouteConfig = RouteConfig;

  @Input('isMobile') isMobile = false;

  @Input() categoryTree: CategoryTree[];

  constructor(
    private megaMenuService: MegaMenuService,
    public authService: AuthService,
    private router: Router
  ) {
    this.router.events.subscribe((event: any) => {
      if (event instanceof NavigationEnd) {
        if (this.isMobile) {
          JsLoader.hideMobileMenu();
        }
      }
    });
  }

  async ngOnInit(): Promise<void> {
    console.log("cateehehds", this.categoryTree);
    if (this.isMobile) {
      setTimeout(() => {
        JsLoader.loadMobileMenuJs();
      }, 500);
    }
  }
}
