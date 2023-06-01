import { Injectable } from '@angular/core';
import {Urls} from '../../shared/config/url';
import {HttpClient} from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class MegaMenuService {

  baseCategoryUrl = Urls.categories;

  constructor(private http: HttpClient) {
  }


  getCategoryTree(): Promise<any> {
    const url = `${Urls.baseUrl}category-tree`;
    return new Promise((resolve, reject) => {
      this.http.get<any>(url).subscribe(
        (res) => {
          resolve(res);
        },
        (err) => reject(err)
      );
    });
  }


}
