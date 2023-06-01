import { Injectable } from '@angular/core';
import { Urls } from '../shared/config/url';
import { HttpClient } from '@angular/common/http';
import { Pagination } from '@modals/pagination.modal';
import { Subscription } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ProductService {
  baseUrl = Urls.products;
  // getProductList$: Subscription;

  private getProductList$: Subscription = null;

  constructor(private http: HttpClient) {}

  getProductByIdForQuickView(productId): Promise<any> {
    return new Promise((resolve, reject) => {
      this.http.get<any>(`${Urls.products}/${productId}?token=true`).subscribe(
        (res) => {
          resolve(res);
        },
        (err) => reject(err)
      );
    });
  }

  getProductBySlugForProductDetail(slug): Promise<any> {
    return new Promise((resolve, reject) => {
      this.http.get<any>(`${Urls.productBySlug}/${slug}?token=true`).subscribe(
        (res) => {
          resolve(res);
        },
        (err) => reject(err)
      );
    });
  }

  getProductList(
    pagination: Pagination = { per_page: 1, current_page: 1 },
    data = null
  ): Promise<any> {
    let url = `${Urls.products}${this.encodeQueryData(data)}`;
    const page = `limit=${pagination.per_page}&page=${pagination.current_page}`;
    url = data ? `${url}&${page}` : `${url}?${page}`;
    return new Promise((resolve, reject) => {
      if (this.getProductList$) {
        this.getProductList$.unsubscribe();
        //this.getProductList$ = null;
      }
      this.getProductList$ = this.http.get<any>(url).subscribe(
        (res) => {
          resolve(res);
        },
        (err) => reject(err)
      );
    });
  }

  getFeaturedProductListing(data = null): Promise<any> {
    const url = `${this.baseUrl}${this.encodeQueryData(data)}`;
    return new Promise((resolve, reject) => {
      this.http.get<any>(url).subscribe(
        (res) => {
          resolve(res);
        },
        (err) => reject(err)
      );
    });
  }
  getProductSimilarDesign(productId, data = null): Promise<any> {
    const url = `${
      Urls.productSimilarDesign
    }/${productId}${this.encodeQueryData(data)}`;
    return new Promise((resolve, reject) => {
      this.http.get<any>(url).subscribe(
        (res) => {
          resolve(res);
        },
        (err) => reject(err)
      );
    });
  }
  getProductCompleteTheLook(productId, data = null): Promise<any> {
    const url = `${
      Urls.productCompleteTheLook
    }/${productId}${this.encodeQueryData(data)}`;
    return new Promise((resolve, reject) => {
      this.http.get<any>(url).subscribe(
        (res) => {
          resolve(res);
        },
        (err) => reject(err)
      );
    });
  }
  getProductDetail(productId): Promise<any> {
    return new Promise((resolve, reject) => {
      this.http.get<any>(`${Urls.productMoreDetail}/${productId}`).subscribe(
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
      if (d == 'abc') {
        continue;
      }
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
