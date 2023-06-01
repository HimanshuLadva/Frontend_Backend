import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {EventBusEmitService} from '@shared/service/event-bus-emit.service';
import {Urls} from '@shared/config/url';
import {Static} from '@shared/static/static';

@Injectable({
  providedIn: 'root'
})
export class CheckoutService {

  constructor(private http: HttpClient, private eventBusEmitService: EventBusEmitService) {
  }


  saveAddressToCard(data): Promise<any> {
    return new Promise((resolve, reject) => {
      this.http.post<any>(`${Urls.checkout}/save-address?token=true`, data)
        .subscribe(resolve, reject);
    });
  }

  saveShippingMethodToCard(data = Static.DEFAULT_SHIPPING_METHOD_DATA): Promise<any> {
    return new Promise((resolve, reject) => {
      this.http.post<any>(`${Urls.checkout}/save-shipping?token=true`, data)
        .subscribe(resolve, reject);
    });
  }

  savePaymentMethodToCard(data = Static.DEFAULT_PAYMENT_METHOD_DATA): Promise<any> {
    return new Promise((resolve, reject) => {
      this.http.post<any>(`${Urls.checkout}/save-payment?token=true`, data)
        .subscribe(resolve, reject);
    });
  }

  saveOrder(): Promise<any> {
    return new Promise((resolve, reject) => {
      this.http.post<any>(`${Urls.checkout}/save-order?token=true`, {})
        .subscribe(resolve, reject);
    });
  }


}
