import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Urls} from '../../shared/config/url';

@Injectable({
  providedIn: 'root'
})
export class CollectionsService {

  baseUrl = Urls.homepageImages;

  constructor(private http: HttpClient) {
  }

  getCollectionListing(): Promise<any> {
    const url = `${this.baseUrl}?type=collection`;
    return new Promise((resolve, reject) => {
      this.http.get<any>(url).subscribe(
        (res) => {
            resolve(res.data);
        },
        (err) => reject(err)
      );
    });
  }
}
