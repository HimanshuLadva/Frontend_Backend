import { BehaviorSubject } from 'rxjs';
import { Component, OnInit } from '@angular/core';
import { CartService } from '../services/cart.service';
import { HomeService } from '../services/home.service';
import { WishlistService } from '../services/wishlist.service';
import { JsLoader } from '../shared/js-loader';
import { LocalstorageService } from '../services/localstorage.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  data:any;
  recommendedForYou:any[] =[];
  resentFood:any[]=[];
  listOfFood:any[] =[];
  cartMsg:boolean = false;
  wishMsg:boolean = false;
  message:string[] = ['Add to ', 'Remove From '];
  isProductInCart:boolean = false;
  isProductInWish:boolean = false;
  wishCartUpdate = new BehaviorSubject<any>([]);

  constructor(private homeService: HomeService, private wishService:WishlistService, private cartService:CartService, private _localStorage: LocalstorageService) {
    this.resentFood = this._localStorage.getDataFromLocalStorage('resentFood');
    this.resentFood.forEach(ele  => {
      this.wishService.wishList.findIndex(e => e.id == ele.id)==-1 ? ele.isInWishList= false:ele.isInWishList= true;
      this.cartService.cartList.findIndex(e => e.id == ele.id)==-1 ? ele.isInCartList= false:ele.isInCartList= true;
    })
   }

  async ngOnInit() {
    await this.setInitialData();
  }

  async setInitialData() {
    JsLoader.sliderJs();
    this.data = await this.homeService.getBestForYou();
    this.recommendedForYou = this.data;
    this.recommendedForYou.forEach(ele  => {
      this.wishService.wishList.findIndex(e => e.id == ele.id)==-1 ? ele.isInWishList= false:ele.isInWishList= true;
      this.cartService.cartList.findIndex(e => e?.data?.id == ele.id)==-1 ? ele.isInCartList= false:ele.isInCartList= true;
    })
  }

  addToWishList(arr:any,id: string) {
    const index = arr.findIndex((ele: any) => ele.id == id);
     this.wishService.addFoodToWishList(arr[index]);
     arr[index].isInWishList = !arr[index].isInWishList;
     this.isProductInWish =arr[index].isInWishList;
     this.checkInResentFood(arr[index]);
     this.wishMsg = true;
    setTimeout(() => {
      this.wishMsg = false;
    }, 1500);
  }

  addToCartList(arr:any, id: string) {
    const index = arr.findIndex((ele: any) => ele.id == id);
    this.cartService.addFoodToCartList(arr[index], 1);
    arr[index].isInCartList = !arr[index].isInCartList;
    this.isProductInCart =arr[index].isInCartList;
    this.checkInResentFood(arr[index]);
    this.cartMsg = true;
    setTimeout(() => {
      this.cartMsg = false;
    }, 1500);
  }

  checkInResentFood(obj:any) {
    let index = this.resentFood.findIndex(ele => ele.id == obj.id);
    this.resentFood[index] = obj;
    index = this.recommendedForYou.findIndex(ele => ele.id == obj.id);
    this.recommendedForYou[index] = obj
  }
  
}

// addToWishList1(id: string) {
//   const index = this.resentFood.findIndex((ele: any) => ele.id == id);
//    this.wishService.addFoodToWishList(this.resentFood[index]);
//    this.resentFood[index].isInWishList = !this.resentFood[index].isInWishList;
//    this.isProductInWish1 =this.resentFood[index].isInWishList;
//    this.wishMsg1 = true;
//   setTimeout(() => {
//     this.wishMsg1 = false;
//   }, 1500);
// }

// addToCartList1(id: string) {
//   const index = this.resentFood.findIndex((ele: any) => ele.id == id);
//   this.cartService.addFoodToCartList(this.resentFood[index], 1);
//   this.resentFood[index].isInCartList = !this.resentFood[index].isInCartList; 
//   this.isProductInCart1 =this.resentFood[index].isInCartList;
//   this.cartMsg1 = true;
//   setTimeout(() => {
//     this.cartMsg1 = false;
//   }, 1500);
// }
