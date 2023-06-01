import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { LocalstorageService } from './localstorage.service';

@Injectable({
  providedIn: 'root',
}) 
export class WishlistService {

  wishList:any[] =[];
  productList = new BehaviorSubject<any>([]);

  constructor(private _localStorage: LocalstorageService) { 
    // const _wishData = localStorage.getItem('wishList');
    // if(_wishData)
    //  this.wishList = (JSON.parse(_wishData))
    this.wishList = this._localStorage.getDataFromLocalStorage('wishList');
  }

  addFoodToWishList(obj:any) {
    console.log("wishlist", this.wishList);
    
     if(this.wishList.length==0 || (this.wishList.findIndex((ele) => ele.id == obj.id)==-1 && this.wishList.length!=0)) {
       this.wishList.push(obj);
       this.setInLocalStorage();   
       this.productList.next(this.wishList);
       
      } else {
        console.log("this is my double add method");
        
        this.deleteFromWishList(obj?.id);
      }
      this.setInLocalStorage();
  }
  
  deleteFromWishList(id:string) {
    this.wishList = this.wishList.filter((ele:any) => ele.id!=id);
    this.setInLocalStorage()
    this.productList.next(this.wishList);
    // if(this.wishList.length ==1) this.wishList = [];
  }
  getWishListCount() {
    return this.productList.asObservable();
  }

  setInLocalStorage() {
    this._localStorage.setDataInLocalStorage('wishList', this.wishList);
    // localStorage.setItem('wishList', JSON.stringify([...this.wishList])); 
  }
}
