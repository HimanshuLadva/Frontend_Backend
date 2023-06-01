import { Component, OnInit } from '@angular/core';
import { ProductService } from '@services/product.service';
import { Product } from '@modals/product-slider.modal';
import { Pagination } from '@modals/pagination.modal';
import { NavigationEnd, Router, ActivatedRoute } from '@angular/router';
import { Static } from '@shared/static/static';
import { EventBusService } from '@shared/service/event-bus/event-bus.service';
import { BusEvents } from '@shared/service/event-bus/bus-events';
import { FilterResponse } from '@modals/filter.modal';
import { EventBusEmitService } from '@shared/service/event-bus-emit.service';
import { Title, Meta } from '@angular/platform-browser';

declare var $;

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.scss'],
})
export class ProductListComponent implements OnInit {
  productList: Product[] = [];
  isLoading = true;
  pagination: Pagination = Static.PRODUCT_LIST_PAGINATION_OBJ;
  filterData;
  infiniteScrollLoading = false;
  filtersWithCategory: FilterResponse;

  constructor(
    private productService: ProductService,
    private router: Router,
    private eventBus: EventBusService,
    public eventBusEmitService: EventBusEmitService,
    private title: Title,
    private meta: Meta,
    private route :ActivatedRoute
  ) {
    this.router.events.subscribe((event: any) => {
      if (event instanceof NavigationEnd) {
        this.pagination = Static.PRODUCT_LIST_PAGINATION_OBJ;
      }
    });
    this.listenEventBus();
  }

  async ngOnInit(): Promise<void> {
    let title = this.route.snapshot.paramMap.get("slug")
    // await this.loadProduct();
    this.title.setTitle(`${title[0].toUpperCase() + title.slice(1)} | Personalized Fine Jewellery`);
    this.meta.addTag(
      {
        name:'keywords',
        content:"fine jewelry, personalized jewelry, mothers rings, mothers ring, mother's ring, mother's rings, birthstone jewelry mothers birthstone jewelry, family jewelry"
      }
    );
    this.listenEventBus();
    this.loadJS();
  }

  async listenEventBus(): Promise<void> {
    this.eventBus.on(
      BusEvents.productListCategoryBanner,
      (filtersWithCategory: FilterResponse) => {
        this.filtersWithCategory = filtersWithCategory;
      }
    );
  }

  async loadProduct(): Promise<void> {
    try {
      this.infiniteScrollLoading = true;
      const res = await this.productService.getProductList(this.pagination, {
        ...this.filterData,
        hide: 'categories,images,variants,superattributes',
      });
      this.productList = [...this.productList, ...res.data];
        if(this.productList.length==0) {
          this.router.navigate(['not-found']);
        }
      this.pagination = res.meta;
      this.isLoading = false;
      this.infiniteScrollLoading = false;
      
      this.loadJS();
    } catch (e) {
      console.error('::::AAA::::BBB::::', e);
    }
  }

  async handleFilterChange(event): Promise<void> {
    this.filterData = event;
    this.pagination = Static.PRODUCT_LIST_PAGINATION_OBJ;
    this.productList = [];
    await this.loadProduct();
  }

  loadJS(): void {
    // product view mode change js
    $('.product-view-mode a').unbind('click', function (e) {});
    $('.product-view-mode a').on('click', function (e) {
      e.preventDefault();
      const shopProductWrap = $('.shop-product-wrap');
      const viewMode = $(this).data('target');
      $('.product-view-mode a').removeClass('active');
      $(this).addClass('active');
      shopProductWrap.removeClass('grid-view list-view').addClass(viewMode);
    });
  }

  onScroll({ currentScrollPosition }): any {
    if (
      this.pagination.last_page != this.pagination.current_page &&
      !this.infiniteScrollLoading
    ) {
      this.pageChange(this.pagination.current_page + 1);
    }
  }

  async pageChange(newPage): Promise<void> {
    this.pagination.current_page = newPage;
    await this.loadProduct();
  }
}
