import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import {OrderService} from '@services/order.service';
import {Order} from '@modals/order.modal';
import {RouteConfig} from '@shared/config/route-config';

@Component({
  selector: 'app-order-detail',
  templateUrl: './order-detail.component.html',
  styleUrls: ['./order-detail.component.scss']
})
export class OrderDetailComponent implements OnInit {

  RouteConfig = RouteConfig;

  isLoading = true;
  orderId;
  order: Order;

  constructor(private route: ActivatedRoute, private router: Router, private orderService: OrderService) {
  }

  async ngOnInit(): Promise<void> {
    this.orderId = this.route.snapshot.paramMap.get('orderId');
    await this.loadOrder();
  }

  async loadOrder(): Promise<void> {
    try {
      this.isLoading = true;
      const res = await this.orderService.getOrderById(this.orderId);
      this.order = res.data;
      this.isLoading = false;
    } catch (e) {
      console.error('::::AAA::::BBB::::', e);
    }
  }
}
