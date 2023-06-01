import { Injectable } from '@angular/core';
import { Urls } from '../../shared/config/url';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class HomeGeneralService {
  baseUrl = Urls.homepageImages;

  constructor(private http: HttpClient) {}

  getHomePageStructure(): Promise<any> {
    const url = `${Urls.baseUrl}orderOfItemsInHomepage`;
    return new Promise((resolve, reject) => {
      this.http.get<any>(url).subscribe(
        (res) => {
          resolve(res);
        },
        (err) => reject(err)
      );
    });
  }

  getCollectionListing(): Promise<any> {
    const url = `${this.baseUrl}?type=collection`;
    return new Promise((resolve, reject) => {
      this.http.get<any>(url).subscribe(
        (res) => {
          resolve(res);
        },
        (err) => reject(err)
      );
    });
  }

  getBlogListing(): Promise<any> {
    const url = `${this.baseUrl}?type=blog`;
    return new Promise((resolve, reject) => {
      this.http.get<any>(url).subscribe(
        (res) => {
          
          resolve(res);
        },
        (err) => reject(err)
      );
    });
  }

  getSliderListing(): Promise<any> {
    let url;
    if (window.screen.width < 1000) {
      url = `${this.baseUrl}?type=mobile-slider`;
    } else {
      url = `${this.baseUrl}?type=slider`;
    }
    return new Promise((resolve, reject) => {
      this.http.get<any>(url).subscribe(
        (res) => {
          resolve(res);
        },
        (err) => reject(err)
      );
    });
  }

  getShowcaseListing(): Promise<any> {
    const url = `${this.baseUrl}?type=showcase`;
    return new Promise((resolve, reject) => {
      this.http.get<any>(url).subscribe(
        (res) => {
          resolve(res);
        },
        (err) => reject(err)
      );
    });
  }

  get3ImagesListing(): Promise<any> {
    const url = `${this.baseUrl}?type=3-images`;
    return new Promise((resolve, reject) => {
      this.http.get<any>(url).subscribe(
        (res) => {
          resolve(res);
        },
        (err) => reject(err)
      );
    });
  }

  get2ImagesListing(): Promise<any> {
    const url = `${this.baseUrl}?type=2-images`;
    return new Promise((resolve, reject) => {
      this.http.get<any>(url).subscribe(
        (res) => {
          resolve(res);
        },
        (err) => reject(err)
      );
    });
  }

  get1ImagesListing(): Promise<any> {
    const url = `${this.baseUrl}?type=1-image`;
    return new Promise((resolve, reject) => {
      this.http.get<any>(url).subscribe(
        (res) => {
          resolve(res);
        },
        (err) => reject(err)
      );
    });
  }

  getTestimonials(): Promise<any> {
    const url = `${this.baseUrl}?type=testimonial`;
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
