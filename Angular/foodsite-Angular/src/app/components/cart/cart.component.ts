import { Component, OnInit } from '@angular/core';
import { CartService } from 'src/app/services/cart.service';
import { LocalstorageService } from 'src/app/services/localstorage.service';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.scss'] 
})
export class CartComponent implements OnInit {
  total_amount:number=0;
  productCount:number=1;
  foodList:any[] =[];
  
  constructor(private cartService:CartService,private _localStorage: LocalstorageService) { }

  ngOnInit(): void {
     this.displayCartProducts();
  }

  displayCartProducts() {
    this.foodList = [...this.cartService.cartList];
     
     this.foodList.forEach(ele => {
        this.total_amount += (ele?.data?.price * ele.quantity);
     })
  }

  deleteFromCart(id:string) {
    this.total_amount -= (this.foodList.filter((ele:any) => ele?.data?.id==id)[0].data?.price * this.foodList.filter((ele:any) => ele?.data?.id==id)[0].quantity)
    this.foodList = this.foodList.filter((ele:any) => ele?.data?.id!=id);
    this.cartService.deleteFromCartList(id);
    this._localStorage.setDataInLocalStorage('cartList', this.foodList);
  }
   
  addIntoCart(obj:any) {
    this.foodList.filter(ele => ele?.data?.id == obj.id)[0].quantity++;
    this.total_amount += (this.foodList.filter(ele => ele?.data?.id == obj.id)[0]?.data?.price)
    this._localStorage.setDataInLocalStorage('cartList', this.foodList);
  }
  removeFromCart(obj:any) {
    if(this.foodList.filter(ele => ele?.data?.id == obj.id)[0].quantity > 1) {
      this.foodList.filter(ele => ele?.data?.id == obj.id)[0].quantity--;
      this.total_amount -= (this.foodList.filter(ele => ele?.data?.id == obj.id)[0]?.data?.price);
    }
    this._localStorage.setDataInLocalStorage('cartList', this.foodList);
  }
}
