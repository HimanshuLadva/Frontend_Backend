import {Component, OnInit} from '@angular/core';
import {AuthService} from '@auth/service/auth.service';
import {OrderService} from '@services/order.service';
import {Order} from '@modals/order.modal';
import {RouteConfig} from '@shared/config/route-config';

@Component({
  selector: 'app-account-order-tab',
  templateUrl: './account-order-tab.component.html',
  styleUrls: ['./account-order-tab.component.scss']
})
export class AccountOrderTabComponent implements OnInit {

  RouteConfig = RouteConfig;
  isLoading = true;
  orderList: Order[] = [];

  constructor(public authService: AuthService, private orderService: OrderService) {
  }

  async ngOnInit(): Promise<void> {
    await this.loadAllOrders();
  }


  async loadAllOrders(): Promise<void> {
    try {
      this.isLoading = true;
      const res = await this.orderService.getOrders();
      this.orderList = res.data;
      this.isLoading = false;
    } catch (e) {
      console.error('::::AAA::::BBB::::', e);
    }
  }

}
