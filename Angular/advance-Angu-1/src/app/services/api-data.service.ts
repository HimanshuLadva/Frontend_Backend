import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ApiDataService {
  url="https://covid-19.dataflowkit.com/v1";
  constructor(private http: HttpClient) { }
  users() {
    return this.http.get(this.url);
  }
  saveUser(newUser: string) {
    return this.http.post(this.url, newUser)
  }
}
