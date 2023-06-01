import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { ProductService } from '@services/product.service';
import { CategoryService } from '@shared/service/category.service';
import { Category } from '@modals/category.modal';
import { FilterResponse } from '@modals/filter.modal';
import { ActivatedRoute, NavigationEnd, Router } from '@angular/router';
import { Static } from '@shared/static/static';
import { EventBusEmitService } from '@shared/service/event-bus-emit.service';

@Component({
  selector: 'app-filter',
  templateUrl: './filter.component.html',
  styleUrls: ['./filter.component.scss'],
})
export class FilterComponent implements OnInit {
  @Output() filterChanged = new EventEmitter<any>();
  DEFAULT_SHOW_COUNT = Static.FILTER_CATEGORY_SHOW_COUNT;
  DEFAULT_SELECTED_CATEGORY = Static.FILTER_CATEGORY_DEFAULT_SELECTED_INDEX;
  showCount = this.DEFAULT_SHOW_COUNT;
  categoryList: Category[];
  filterList: FilterResponse;
  priceMin = 0;
  priceMax = 0;
  routeCategoryId;
  routeCategorySlug;
  isLoading = true;
  categoryId;
  categorySlug;
  filterData = [];
  showMobileFilter = false;

  constructor(
    private productService: ProductService,
    private categoryService: CategoryService,
    private route: ActivatedRoute,
    private router: Router,
    private eventBusEmitService: EventBusEmitService
  ) {
    this.router.events.subscribe((val) => {
      if (val instanceof NavigationEnd) {
        window.scrollTo(0, 0);
        this.reloadAllData();
      }
    });
  }

  async ngOnInit(): Promise<void> {}

  async reloadAllData(): Promise<void> {
    this.routeCategorySlug = this.route.snapshot.paramMap.get('slug');
    if (this.routeCategorySlug) {
      this.categorySlug = this.routeCategorySlug;
      this.loadFilters().then(() => {
        this.emitEvent();
        this.isLoading = false;
      });
    } else {
      await this.loadCategoryList();
    }
  }

  async loadCategoryList(): Promise<void> {
    try {
      const res = await this.categoryService.getAllCategory();
      this.categoryList = res.data;
      this.categoryList = this.categoryList.filter((d) => d.slug != 'root');
      this.categoryId = this.categoryList[this.DEFAULT_SELECTED_CATEGORY].id;
      this.categorySlug =
        this.categoryList[this.DEFAULT_SELECTED_CATEGORY].slug;
      await this.loadFilters();
      this.emitEvent();
      this.isLoading = false;
    } catch (e) {
      console.error('::::AAA::::BBB::::', e);
    }
  }

  async handleCategoryChange(categoryId): Promise<void> {
    this.categoryId = categoryId;
    this.categorySlug = this.categoryList.find((x) => x.id == categoryId).slug;
    await this.loadFilters();
    this.emitEvent();
  }

  async handleAttributeChange(event): Promise<void> {
    const index = this.filterData.findIndex((d) => d.code == event.code);
    if (index > -1) {
      if (event.value == '') {
        this.filterData.splice(index, 1);
      } else {
        this.filterData[index] = event;
      }
    } else {
      this.filterData.push(event);
    }
    this.emitEvent();
  }

  async handleMobileFilterChange(event): Promise<void> {
    const temp = this.filterData.find((x) => x.code === 'price');
    this.filterData = temp ? [temp] : [];
    event?.forEach((x) => {
      this.filterData.push({
        code: x.attributeCode,
        value: x.selectedOptions.toString(),
      });
    });
    console.clear();
    this.emitEvent();
  }

  async loadFilters(): Promise<void> {
    try {
      const res = await this.categoryService.getFiltersByCategorySlug(
        this.categorySlug
      );
      this.filterList = res.data;
      this.categoryId = this.filterList.id;
      this.priceMin = Number(res.min_price);
      this.priceMax = Number(res.max_price);
      this.eventBusEmitService.productListCategoryBanner(this.filterList);
    } catch (e) {
      console.error('::::AAA::::BBB::::', e);
    }
  }

  handlePriceChange(event: any): void {
    const i = this.filterData?.findIndex((d) => d.code == 'price');
    if (i > -1) {
      this.filterData[i].value = `${event.price_min},${event.price_max}`;
    } else {
      this.filterData.push({
        code: 'price',
        value: `${event.price_min},${event.price_max}`,
      });
    }
    this.emitEvent();
  }

  showMore(isLess = false): void {
    if (isLess) {
      this.showCount = this.DEFAULT_SHOW_COUNT;
    } else {
      this.showCount =
        this.categoryList?.length > this.DEFAULT_SHOW_COUNT
          ? this.categoryList?.length
          : this.DEFAULT_SHOW_COUNT;
    }
  }

  private emitEvent(): void {
    const data = [];
    if (this.categoryId > 0) {
      data['category_id'] = this.categoryId;
    }
    this.filterData.forEach((d) => (data[d.code] = d.value));
    this.filterChanged.emit(data);
  }
}
