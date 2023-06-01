import { Component, OnInit } from '@angular/core';
import { Product } from '@modals/product.modal';
import { Pagination } from '@modals/pagination.modal';
import { ProductService } from '@services/product.service';
import { SearchService } from '@shared/service/search.service';
import { ActivatedRoute, NavigationEnd, Router } from '@angular/router';
import { Static } from '@shared/static/static';

declare var $;

@Component({
  selector: 'app-search-product-list',
  templateUrl: './search-product-list.component.html',
  styleUrls: ['./search-product-list.component.scss'],
})
export class SearchProductListComponent implements OnInit {
  productList: Product[] = [];
  isLoading = true;
  pagination: Pagination = Static.PRODUCT_SEARCH_LIST_PAGINATION_OBJ;
  filterData;
  searchKeyword;
  infiniteScrollLoading = false;
  isThereAnyProduct = false;
  constructor(
    private productService: ProductService,
    private searchService: SearchService,
    private router: Router,
    private route: ActivatedRoute
  ) {
    this.router.events.subscribe((event: any) => {
      if (event instanceof NavigationEnd) {
        this.pagination = Static.PRODUCT_SEARCH_LIST_PAGINATION_OBJ;
      }
    });
  }

  async ngOnInit(): Promise<void> {
    this.router.events.subscribe((event) => {
      if (event instanceof NavigationEnd) {
        this.searchKeyword = this.route.snapshot.paramMap.get('keyword');
        this.productList = [];
        this.loadProduct().then(() => {});
      }
    });
    this.searchKeyword = this.route.snapshot.paramMap.get('keyword');
    await this.loadProduct();
    // this.loadJS();
  }

  async loadProduct(): Promise<void> {
    try {
      this.infiniteScrollLoading = true;
      const res = await this.searchService.searchAll(
        this.searchKeyword,
        this.pagination
      );
      this.productList = [...this.productList, ...res.data];
      this.pagination = res.meta;
      this.infiniteScrollLoading = false;
      if(this.productList.length) {
        this.isThereAnyProduct = true;
      } 
      this.isLoading = false;
    } catch (e) {
      console.error('::::AAA::::BBB::::', e);
    }
  }

  onScroll(): any {
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
  loadJS(): void {
    // product view mode change js
    $('.product-view-mode a').on('click', function (e) {
      e.preventDefault();
      const shopProductWrap = $('.shop-product-wrap');
      const viewMode = $(this).data('target');
      $('.product-view-mode a').removeClass('active');
      $(this).addClass('active');
      shopProductWrap.removeClass('grid-view list-view').addClass(viewMode);
    });
  }
}
