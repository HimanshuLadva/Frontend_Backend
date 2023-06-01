import { Injectable } from '@angular/core';
import { dataType } from '../datatype';

@Injectable({
  providedIn: 'root'
})
export class UserDataService {

  constructor() { }
  getData() {
    const data:dataType = {
      name: "himanshu",
      lname: "ladva",
      age: 21,
      address: "shapar-veraval",
    }
    return data;
  }
}
