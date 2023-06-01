import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Urls} from '@shared/config/url';
import {UpdatePassword} from '@modals/user.modal';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {

  constructor(private http: HttpClient) {
  }

  getAllAddress(): Promise<any> {
    return new Promise((resolve, reject) => {
      this.http.get(`${Urls.address}?token=true`).subscribe(resolve, reject);
    });
  }

  getAllCountries(): Promise<any> {
    return new Promise((resolve, reject) => {
      this.http.get(`${Urls.country}`).subscribe(resolve, reject);
    });
  }

  updateProfile(data): Promise<any> {
    return new Promise((resolve, reject) => {
      this.http.put(`${Urls.customer}/profile?token=true`, data).subscribe(resolve, reject);
    });
  }

  updatePassword(data: UpdatePassword): Promise<any> {
    return new Promise((resolve, reject) => {
      this.http.post(`${Urls.customer}/resetPassword?token=true`, data).subscribe(resolve, reject);
    });
  }

  getCityStateFromPostcode(postcode): Promise<any> {
    return new Promise((resolve, reject) => {
      this.http.get(`${Urls.get_city_state_from_postcode}?pincode=${postcode}`).subscribe(resolve, reject);
    });
  }

  saveAddress(data): Promise<any> {
    return new Promise((resolve, reject) => {
      this.http.post(`${Urls.address}/create?token=true`, data).subscribe(resolve, reject);
    });
  }

  updateAddress(data, id): Promise<any> {
    return new Promise((resolve, reject) => {
      this.http.put(`${Urls.address}/${id}?token=true`, data).subscribe(resolve, reject);
    });
  }
}
