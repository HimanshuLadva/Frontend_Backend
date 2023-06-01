import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class LocalstorageService {

  constructor() { }

  setDataInLocalStorage(key:string,data:any) {
    localStorage.setItem(`${key}`, JSON.stringify(data));
  }

  getDataFromLocalStorage(key:string) {
    const data = localStorage.getItem(`${key}`);
    if(data) {
      return JSON.parse(data);
    }
    if(key=='view' && !data) {
      return 'list';
    }
    return [];
  }
}
