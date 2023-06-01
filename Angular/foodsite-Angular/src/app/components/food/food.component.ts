import { Component, OnInit } from '@angular/core';
import { CartService } from 'src/app/services/cart.service';
import { HomeService } from 'src/app/services/home.service';
import { WishlistService } from 'src/app/services/wishlist.service';
import { ActivatedRoute, Router, NavigationEnd } from '@angular/router';
import { Title } from '@angular/platform-browser';
import { SubSink } from 'subsink';
import { LocalstorageService } from 'src/app/services/localstorage.service';

@Component({
  selector: 'app-food',
  templateUrl: './food.component.html',
  styleUrls: ['./food.component.scss'],
})
export class FoodComponent implements OnInit {
  data: any;
  listOfFood: any[] = [];
  food: any;
  page: number = Number(this._route.snapshot.queryParamMap.get('page')) ?? 1;
  productPerPage:number = Number(this._route.snapshot.queryParamMap.get('limit')) ?? 8;
  search = this._route.snapshot.queryParamMap.get('search') || '';
  total: number = 0;
  slug:any;
  cartMsg:boolean = false;
  wishMsg:boolean = false;
  isProductInCart:boolean = false;
  isProductInWish:boolean = false;
  productPerPageArr = [8, 12, 16, 20];
  message:string[] = ['Add to ', 'Remove From '];
  isLoading=true;
  viewOfPage= 'list';
  subs = new SubSink();

  constructor(
    private homeService: HomeService,
    private wishService: WishlistService,
    private cartService: CartService,
    private _route: ActivatedRoute,
    private _router: Router,
    private title:Title,
    private _localStorage: LocalstorageService
  ) {

   this.subs.sink = this._router.events.subscribe((event)=>{
    if(event instanceof NavigationEnd) {
      this.title.setTitle(this._route.snapshot.paramMap.get('slug'))
      const {queryParams} = this._router.routerState.snapshot.root;
      this.page = +queryParams['page'] ? +queryParams['page'] : 1;
      this.productPerPage = +queryParams['limit'] ? +queryParams['limit'] : 8;
      this.search = queryParams['search'] ? queryParams['search'] :'';
      this.loadData();
    }
   });
  }
  
  ngOnInit():void {
    console.log("ngOninit");
    
    // this._router.navigate([], {
    //   relativeTo: this._route,
    //   queryParams: {
    //     page: Number(this._route.snapshot.queryParamMap.get('page')) || 1,
    //     limit: Number(this._route.snapshot.queryParamMap.get('limit')) || 8,
    //     search:this._route.snapshot.queryParamMap.get('search')==''?null: this._route.snapshot.queryParamMap.get('search'),
    //   },
    //   queryParamsHandling:'merge'
    // })
    this.viewOfPage = this._localStorage.getDataFromLocalStorage('view') || 'list';
  }

  
  itemPerPage(event:any) {
    this.productPerPage = event.target.value;
    this.page = 1;
    this._router.navigate([`menu/${this.slug}`],
        {
          queryParams: {
            page: this.page,
            limit:this.productPerPage,
            search:this.search==''?null:this.search,
          }, 
          queryParamsHandling:'merge'
        } 
        )
  }

  async loadData() {
    console.log("load data");
     this.slug = this._route.snapshot.paramMap.get('slug');
     if(this.search!='') {
       console.log("search", this.search);
       this.listOfFood = await this.homeService.getAllSearchFood(this.slug, this.search);
       this.isLoading =false;
       this.total = this.listOfFood.length; 
       console.log("ourfoods total", this.total);
      } else {
        this.listOfFood = await this.homeService.getAllFoodWishCategory(this.slug , this.page, this.productPerPage);
        this.isLoading =false;
        this.total = await this.homeService.getPagination(this._route.snapshot.paramMap.get('slug')); 
      }
      this.listOfFood.forEach(ele => {
        this.wishService.wishList.findIndex(e => e.id == ele.id)==-1 ? ele.isInWishList= false:ele.isInWishList= true;
        this.cartService.cartList.findIndex(e => e.id == ele.id)==-1 ? ele.isInCartList= false:ele.isInCartList= true;
      })
    }

  searchInput(box: any) {
    if(!(+box.value)) {
      this.search = box.value;
      this.page = 1;
      this._router.navigate([`menu/${this.slug}`],
      {
        queryParams: {
          page: this.page,
            limit:this.productPerPage,
            search:this.search==''?null:this.search,
          }, 
          queryParamsHandling:'merge'
        } 
        )
      }
    }
    
    filterProduct(select:any) {
      console.log("i am called");
      if(select.value == 'price' || select.value == 'rprice')
      select.value == 'price'? this.listOfFood.sort((a, b) => a.price > b.price ? 1 : -1) : this.listOfFood.sort((a, b) => a.price > b.price ? -1 : 1);
      else
      select.value == 'rate'? this.listOfFood.sort((a, b) => a.rate > b.rate ? 1 : -1) : this.listOfFood.sort((a, b) => a.rate > b.rate ? -1 : 1); 
    }
    
    addToWishList(id: string) {
      const index = this.listOfFood.findIndex((ele: any) => ele.id == id);
      this.wishService.addFoodToWishList(this.listOfFood[index]);
      this.listOfFood[index].isInWishList = !this.listOfFood[index].isInWishList;
      this.isProductInWish =this.listOfFood[index].isInWishList;
      this.wishMsg = true;
      
      setTimeout(() => {
        this.wishMsg = false;
      }, 1500);
    }
    
    addToCartList(id: string) {
      const index = this.listOfFood.findIndex((ele: any) => ele.id == id);
    this.cartService.addFoodToCartList(this.listOfFood[index], 1);
    this.listOfFood[index].isInCartList = !this.listOfFood[index].isInCartList;
    this.isProductInCart = this.listOfFood[index].isInCartList;
    this.cartMsg = true;
    setTimeout(() => {
      this.cartMsg = false;
    }, 1500);
  }
  
  pageChangeEvent(event: number) {
    this.page = event;
      console.log("you are enter");

      this._router.navigate([], {
        relativeTo: this._route,
        queryParams: {
          page: this.page===0 ? null :this.page,
          limit:this.productPerPage===0 ? null : this.productPerPage,
          search:this.search==''?null:this.search,
        },
        queryParamsHandling:'merge'
      })
      
    }
    
    changeView(view:string) {
      this.viewOfPage = view == 'list'?'list':'grid';
      this._localStorage.setDataInLocalStorage('view', view);
    }

    ngOnDestroy():void {
       this.subs.unsubscribe(); 
    }
  }