import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject } from 'rxjs';
import { map } from 'rxjs/operators';



@Injectable({
  providedIn: 'root'
})
export class AccountService {

  // properties needed
  private baseUrlLogin = 'https://localhost:44309/api/Account/FacebookLogin';
  private loginStatus = new BehaviorSubject<boolean>(this.getLoginStatus());
  private username = new BehaviorSubject<string>(localStorage.getItem('username'));

  // communicate with web api
  constructor(private http: HttpClient) { }

  getLoginStatus(): boolean {
    return false;
  }

  get CurrentUsername() {
    return this.username;
  }
  // Login method that sends the data to out API
  Login(accessToken:any) {
    return this.http.post<any>(this.baseUrlLogin, accessToken).pipe(
      map(result => {
        console.log("this is login", result);
        
        if (result && result.message != null) {
          console.log(result);
          this.loginStatus.next(true);
          localStorage.setItem('username', result.data.userName);
          localStorage.setItem('token', result.data.accessToken);
          console.log('We sent a message to our Controller API. It works');
        }
        return result;
      })
    );
  }

  Logout() {
    this.loginStatus.next(false);
    localStorage.removeItem('token');
    localStorage.removeItem('username');
    console.log('User Logged out successfully');
  }


}
