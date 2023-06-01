import {Injectable} from '@angular/core';
import {Urls} from '../config/url';
import {HttpClient} from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  constructor(private http: HttpClient) {
  }

  getAllCategory(): Promise<any> {
    return new Promise((resolve, reject) => {
      this.http.get<any>(`${Urls.categories}`).subscribe(
        (res) => {
          resolve(res);
        },
        reject
      );
    });
  }


  getFiltersByCategoryId(categoryId): Promise<any> {
    return new Promise((resolve, reject) => {
      this.http.get<any>(`${Urls.filterAttributes}/${categoryId}`).subscribe(
        (res) => {
          resolve(res);
        },
        reject
      );
    });
  }


  getFiltersByCategorySlug(categorySlug): Promise<any> {
    return new Promise((resolve, reject) => {
      this.http.get<any>(`${Urls.filterAttributesSlug}/${categorySlug}`).subscribe(
        (res) => {
          resolve(res);
        },
        reject
      );
    });
  }

  getAllDescendantCategories(data = null): Promise<any> {
    const url = `${Urls.baseUrl}descendant-categories${this.encodeQueryData(data)}`;
    return new Promise((resolve, reject) => {
      this.http.get<any>(url).subscribe(
        (res) => {
          resolve(res);
        },
        (err) => reject(err)
      );
    });
  }

  encodeQueryData(data): string {
    const ret = [];
    for (const d in data) {
      if (data[d]) {
        ret.push(encodeURIComponent(d) + '=' + encodeURIComponent(data[d]));
      }
    }
    let q = ret.join('&');
    if (q) {
      q = '?' + q;
    }
    return q;
  }
}
