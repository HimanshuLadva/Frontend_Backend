import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {EventBusEmitService} from '@shared/service/event-bus-emit.service';
import {Urls} from '@shared/config/url';

@Injectable({
  providedIn: 'root'
})
export class OrderService {
  constructor(private http: HttpClient, private eventBusEmitService: EventBusEmitService) {
  }

  getOrders(): Promise<any> {
    return new Promise((resolve, reject) => {
      this.http.get<any>(`${Urls.orders}?token=true`)
        .subscribe(resolve, reject);
    });
  }

  getOrderById(orderId): Promise<any> {
    return new Promise((resolve, reject) => {
      this.http.get<any>(`${Urls.orders}/${orderId}?token=true`)
        .subscribe(resolve, reject);
    });
  }

}
