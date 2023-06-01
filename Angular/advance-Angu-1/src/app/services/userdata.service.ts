import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class UserdataService {

  constructor() { }
  users() {
    return [
      {name: "himanshu", email: "HA@gmail.com"},
      {name: "darshit", email: "DR@gmail.com"},
      {name: "vishal", email: "VI@gmail.com"},
      {name: "yash", email: "YA@gmail.com"},
    ]
  }
}
