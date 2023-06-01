import { Component, OnInit } from '@angular/core';
import { WishlistService } from 'src/app/services/wishlist.service';

@Component({
  selector: 'app-wishlist',
  templateUrl: './wishlist.component.html',
  styleUrls: ['./wishlist.component.scss'],
})
export class WishlistComponent implements OnInit {
  foodList: any[] = [];
  constructor(private wishService: WishlistService) {}


  ngOnInit(): void {
    this.setWishProducts();
  }

  setWishProducts() {
    this.foodList = this.wishService.wishList;
  }
  deleteFromWish(id: string) {
    this.foodList = this.foodList.filter((ele: any) => ele.id != id);
    this.wishService.deleteFromWishList(id);
  }
}
