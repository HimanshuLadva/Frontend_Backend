import { Component, OnInit } from '@angular/core';
import { WishlistService } from 'src/app/services/wishlist.service';
import { CartService } from 'src/app/services/cart.service';
import { SubSink } from 'subsink';
import { LocalstorageService } from 'src/app/services/localstorage.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit {

  subs = new SubSink();
  wishListCount:number= 0;
  cartListCount:number= 0;

  constructor(private _wishService: WishlistService, private _cartService: CartService, private _localStorage: LocalstorageService) {    
  }

  ngOnInit(): void {
     this.setProductCount();
  }

  setProductCount() {
    this.subs.sink= this._wishService.getWishListCount().subscribe((res) => {
      console.log("res.length", ((JSON.parse(localStorage.getItem('wishList')|| '[]'))).length);
      this.wishListCount = res.length || this._localStorage.getDataFromLocalStorage('wishList').length;
    });
    this.subs.sink =this._cartService.getCartListCount().subscribe((res) => {
      this.cartListCount = res.length || this._localStorage.getDataFromLocalStorage('cartList').length;
    })
  }
   
  ngOnDestroy():void {
     this.subs.unsubscribe();
  }
}
