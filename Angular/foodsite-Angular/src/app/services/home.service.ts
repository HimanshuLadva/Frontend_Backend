import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment.prod';
import { LocalstorageService } from './localstorage.service';

@Injectable({
   providedIn: 'root'
})
export class HomeService {

   data: any;
   resentFood: any[] = [];
   url = environment.URL;

   constructor(private http: HttpClient, private _localStorage: LocalstorageService) {
      // const data = localStorage.getItem('resentFood');
      // if (data) {
      //    this.resentFood = JSON.parse(data);
      // }
      this.resentFood = this._localStorage.getDataFromLocalStorage('resentFood');
   }

   async getBestForYou() {
      await this.http.get(`${this.url}/best-foods?_limit=10`).toPromise().then((res) => {
         this.data = res;
      });
      return this.data
   }

   async getSpecifyFood(category: string, id: string) {
      await this.http.get(`${this.url}/${category}?id=${id}`).toPromise().then((res) => {
         this.data = res;
      })
      return this.data;
   }

   async getAllFoodWishCategory(category: string, page: number, productPerPage: number) {
      await this.http.get(`${this.url}/${category}?_limit=${productPerPage}&_page=${page}`).toPromise().then((res) => {
         this.data = res;
      })
      return this.data;
   }

   async getAllSearchFood(category: string, search: string) {
      await this.http.get(`${this.url}/${category}?name_like=${search}`).toPromise().then((res) => {
         this.data = res;
      })
      return this.data;
   }

   async getPagination(type: any) {
      await this.http.get(`${this.url}/pagination`).toPromise().then((res) => {
         this.data = res;
      })
      return this.data[type];
   }
}
