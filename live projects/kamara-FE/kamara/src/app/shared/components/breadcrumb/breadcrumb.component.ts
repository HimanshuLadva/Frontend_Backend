import { Component, OnDestroy, OnInit } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';
import { RouteConfig } from '@shared/config/route-config';
import { SubSink } from 'subsink';

interface Breadcrumb {
  route?: string;
  urls?: { name?: string; link?: string; current?: boolean }[];
}

@Component({
  selector: 'app-breadcrumb',
  templateUrl: './breadcrumb.component.html',
  styleUrls: ['./breadcrumb.component.scss'],
})
export class BreadcrumbComponent implements OnInit, OnDestroy {
  subs = new SubSink();
  RouteConfig = RouteConfig;

  current: Breadcrumb;
  all: Breadcrumb[] = [
    {
      route: RouteConfig.auth_login,
      urls: [
        {
          name: 'Login',
          link: RouteConfig.auth_login,
          current: true,
        },
      ],
    },
    {
      route: RouteConfig.auth_register,
      urls: [
        {
          name: 'Register',
          link: RouteConfig.auth_register,
          current: true,
        },
      ],
    },
    {
      route: RouteConfig.auth_account,
      urls: [
        {
          name: 'Account',
          link: RouteConfig.auth_account,
          current: true,
        },
      ],
    },
    {
      route: RouteConfig.wishlist,
      urls: [
        {
          name: 'Shop',
          link: RouteConfig.list,
          current: false,
        },
        {
          name: 'Wishlist',
          link: RouteConfig.wishlist,
          current: true,
        },
      ],
    },
    {
      route: RouteConfig.cart,
      urls: [
        {
          name: 'Shop',
          link: RouteConfig.list,
          current: false,
        },
        {
          name: 'Cart',
          link: RouteConfig.cart,
          current: true,
        },
      ],
    },
    {
      route: RouteConfig.list,
      urls: [
        {
          name: 'Shop',
          link: RouteConfig.list,
          current: true,
        },
      ],
    },
    {
      route: RouteConfig.searchProduct,
      urls: [
        {
          name: 'Shop',
          link: RouteConfig.list,
          current: false,
        },
        {
          name: 'Search',
          link: RouteConfig.searchProduct,
          current: true,
        },
      ],
    },
    {
      route: RouteConfig.checkout,
      urls: [
        {
          name: 'Shop',
          link: RouteConfig.list,
          current: true,
        },
        {
          name: 'Cart',
          link: RouteConfig.cart,
          current: false,
        },
        {
          name: 'Checkout',
          link: RouteConfig.checkout,
          current: true,
        },
      ],
    },
    {
      route: RouteConfig.orderDetail,
      urls: [
        {
          name: 'Shop',
          link: RouteConfig.list,
          current: false,
        },
        {
          name: 'Orders',
          link: RouteConfig.auth_account,
          current: false,
        },
        {
          name: 'Order Detail',
          link: RouteConfig.orderDetail,
          current: true,
        },
      ],
    },
    {
      route: "privacy-policy",
      urls: [
        {
          name: 'privacy-policy',
          link: RouteConfig.policy,
          current: true,
        },
      ],
    },
  ];

  constructor(private router: Router) {}

  ngOnDestroy(): void {
    this.subs.unsubscribe();
  }

  ngOnInit(): void {
    this.setCurrentUrl(this.router.url);
    this.subs.sink = this.router.events.subscribe((event: any) => {
      if (event instanceof NavigationEnd) {
      }
    });
  }

  setCurrentUrl(url: string): void {
    this.all.forEach((r) => {
      if (url.toLowerCase().includes(r.route.toLowerCase())) {
        this.current = r;
      }
    });
  }
}
