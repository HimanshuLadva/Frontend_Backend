import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CartService } from 'src/app/services/cart.service';
import { HomeService } from 'src/app/services/home.service';
import { WishlistService } from 'src/app/services/wishlist.service';
import { Title } from '@angular/platform-browser';
import { ThisReceiver } from '@angular/compiler';
import { LocalstorageService } from 'src/app/services/localstorage.service';

@Component({
  selector: 'app-foodinfo', 
  templateUrl: './foodinfo.component.html',
  styleUrls: ['./foodinfo.component.scss']
})
export class FoodinfoComponent implements OnInit {

  id:any;
  slug:any;
  data:any[] =[];
  index:number = -1;
  cartMsg:boolean = false;
  wishMsg:boolean = false;
  isProductInCart:boolean = false;
  isProductInWish:boolean = false;
  isProductInUpdate:boolean = false;
  message:string[] = ['Add to ', 'Remove From '];
  printDetail:any;
  productCount:number=1;
  isUpdate=false;
  isLoading = true;

  constructor(private route: ActivatedRoute, private homeService:HomeService, private wishService: WishlistService, private cartService:CartService,
     private title:Title,
     private _localStorage: LocalstorageService
    ) { }

  async ngOnInit() {
     await this.setFoodInfo();
  }
  
  async setFoodInfo() {
    this.id = this.route.snapshot.paramMap.get('id');
    this.slug = this.route.snapshot.paramMap.get('slug');
    this.title.setTitle(this.slug+" | "+this.id)
    this.data = await this.homeService.getSpecifyFood(this.slug, this.id);
    this.printDetail = this.data[0];  
    this.isLoading = false; 
    if(this.wishService.wishList.findIndex((ele) => ele.id == this.printDetail.id)!=-1) {
       this.isProductInWish = true; 
    }
    if(this.cartService.cartList.findIndex((ele) => ele?.data?.id == this.printDetail.id)!=-1) {
      this.isProductInCart = true;  
    }
    
    
    this.productCount = this.cartService.cartList[this.cartService.cartList.findIndex(ele =>ele?.data?.id == this.printDetail.id)]?.quantity || 1;

    if(this.homeService.resentFood.length < 5 || this.homeService.resentFood.findIndex(ele => ele.id == this.printDetail.id)!=-1){
      if(this.homeService.resentFood.length==0) {
         this.homeService.resentFood.unshift(this.printDetail)
      }
      else {
        if(this.homeService.resentFood.findIndex(ele => ele.id == this.printDetail.id)==-1) {
          this.homeService.resentFood.unshift(this.printDetail);
        } else {
          this.homeService.resentFood = this.homeService.resentFood.filter(ele => ele.id != this.printDetail.id);
          this.homeService.resentFood.unshift(this.printDetail);
        }
      }
      this._localStorage.setDataInLocalStorage('resentFood', this.homeService.resentFood);
    } 
  }
  add() {
    this.productCount++;
    if(this.cartService.cartList.findIndex(ele =>ele?.data?.id == this.printDetail.id)!=-1) {
       this.isProductInUpdate = true;
    }
  }
  sub() {
    if(this.productCount>1)
    {
      this.productCount--;
    } 
  }

  addToWishList() {
    this.wishService.addFoodToWishList(this.printDetail);
    this.wishMsg = true;
    this.isProductInWish = !this.isProductInWish;
    setTimeout(() => {
      this.wishMsg = false;
    }, 1500);
  }

  addToCartList() {
    this.cartService.addFoodToCartList(this.printDetail, this.productCount);
    this.cartMsg = true;
    this.isProductInCart = !this.isProductInCart;
    
    setTimeout(() => {
      this.cartMsg = false;
    }, 1500);
  }

  upDateToCartList() {
    this.cartService.updateToCartList(this.printDetail,this.productCount);
    this.isProductInUpdate = !this.isProductInUpdate;
    this.cartMsg= true;
    setTimeout(() => {
      this.cartMsg = false;
    }, 1500);
  }

}
