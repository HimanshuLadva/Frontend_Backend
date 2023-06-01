import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Urls } from '@shared/config/url';
import { Pagination } from '@modals/pagination.modal';

@Injectable({
  providedIn: 'root',
})
export class SearchService {
  constructor(private http: HttpClient) {}

  search(keyword): Promise<any> {
    return new Promise((resolve, reject) => {
      this.http
        .get<any>(`${Urls.productSearchSuggestions}/${keyword}`)
        .subscribe((res) => {
          resolve(res);
        }, reject);
    });
  }

  searchAll(
    keyword,
    pagination: Pagination = { per_page: 1, current_page: 1 }
  ): Promise<any> {
    const page = `limit=${pagination.per_page}&page=${pagination.current_page}`;
    return new Promise((resolve, reject) => {
      this.http
        .get<any>(
          `${Urls.products}?search=${keyword}&${page}&hide=categories,description,images,variants,superattributes`
        )
        .subscribe((res) => {
          resolve(res);
        }, reject);
    });
  }
}
