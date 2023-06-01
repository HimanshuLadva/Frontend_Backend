import { Injectable } from '@angular/core';
import { Urls } from '@shared/config/url';
import { HttpClient } from '@angular/common/http';
import { RegisterUser } from '@modals/user.modal';
import { CartService } from '@services/cart.service';
import { Static } from '@shared/static/static';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private login_url = Urls.login;
  private register_url = Urls.register;
  private logout_url = Urls.logout;
  private _user = 'user';
  private _token = 'token';
  private _msg = 'msg';
  private _storage = localStorage;
  constructor(private http: HttpClient) {}

  get User(): RegisterUser {
    return this.getStorage(this._user);
  }

  get Token(): any {
    return this.getStorage(this._token);
  }

  get hideMessage(): any {
    return this.getStorage(this._msg);
  }

  get isUserLogin(): boolean {
    return !!this.User;
  }

  get isUserGuest(): boolean {
    return !this.User;
  }
  setMessage(): any {
    this.setStorage(this._msg, 'true');
  }
  public static getErrorObj(err): any {
    if (err.status == 401) {
      return err.error.error;
    }
    if (err.status == 422) {
      const arr = [];
      for (const index in err.error.errors) {
        arr.push({ key: index, value: err.error.errors[index] });
      }
      return arr;
    }
  }

  login(email: string, password: string): Promise<any> {
    return new Promise((resolve, reject) => {
      this.http.post<any>(this.login_url, { email, password }).subscribe(
        (res) => {
          this.setStorage(this._user, res.data);
          this.setStorage(this._token, res.token);
          location.reload();
          resolve(res);
        },
        (e) => {
          reject({ error: e.error, status: e.status, e });
        }
      );
    });
  }

  async register(data: RegisterUser): Promise<any> {
    try {
      const res = await this.registerCore(data);
      if (
        res.message &&
        res.message === 'Your account has been created successfully.'
      ) {
        return await this.login(data.email, data.password);
      }
    } catch (e) {
      console.error('::::AAA::::BBB::::', e);
      return e;
    }
  }

  registerCore(data: RegisterUser): Promise<any> {
    // "message": "Your account has been created successfully."
    return new Promise((resolve, reject) => {
      this.http.post<any>(this.register_url, data).subscribe(
        (res) => {
          resolve(res);
        },
        (e) => {
          reject({ error: e.error, status: e.status, e });
        }
      );
    });
  }

  logout(): void {
    if (this.isUserLogin) {
      this.removeStorage(this._user);
      this.removeStorage(this._token);
      location.reload();
    }
    // return new Promise((resolve, reject) => {
    //   this.http.get<any>(this.logout_url).subscribe(
    //     (res) => {
    //       this.removeStorage(this._user);
    //       this.removeStorage(this._token);
    //       location.reload();
    //       resolve(res);
    //     },
    //     (error) => {
    //       this.removeStorage(this._user);
    //       this.removeStorage(this._token);
    //       location.reload();
    //       reject(error);
    //     }
    //   );
    // });
  }

  updateUser(data): void {
    this.setStorage(this._user, data);
  }

  private setStorage(key, value): void {
    this._storage.setItem(key, JSON.stringify(value));
  }

  private getStorage(key): any {
    return JSON.parse(this._storage.getItem(key));
  }

  private removeStorage(key): void {
    this._storage.removeItem(key);
  }
}
