import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FilterResponse } from '@modals/filter.modal';
import { Category } from '@modals/category.modal';
import { BusEvents } from '@shared/service/event-bus/bus-events';
import { EventBusService } from '@shared/service/event-bus/event-bus.service';

declare var $;

@Component({
  selector: 'app-mobile-filter',
  templateUrl: './mobile-filter.component.html',
  styleUrls: ['./mobile-filter.component.scss'],
})
export class MobileFilterComponent implements OnInit {
  @Input() filterList: FilterResponse;
  @Input() categoryList: Category[];
  @Input() ceil: number;
  @Input() floor: number;
  @Output() onFilterChange = new EventEmitter<any>();
  @Output() priceChange = new EventEmitter<any>();
  categoryId;
  selectedAttribute: { attributeCode?: number; selectedOptions?: number[] }[] =
    [];

  constructor(private eventBus: EventBusService) {}

  ngOnInit(): void {
    // setTimeout(() => {
    //   this.openFilter();
    // }, 1000);
    this.listenEventBus();
  }

  async listenEventBus(): Promise<void> {
    this.eventBus.on(BusEvents.showMobileFilter, () => {
      this.openFilter();
    });
    this.eventBus.on(BusEvents.hideMobileFilter, () => {
      this.closeFilter();
    });
  }

  async handleCategoryChange(categoryId): Promise<void> {}
  handleAttributeClick(attrId, optionId): void {
    const index = this.selectedAttribute?.findIndex(
      (x) => x.attributeCode === attrId
    );
    if (index > -1) {
      const optionIndex = this.selectedAttribute[
        index
      ].selectedOptions.findIndex((x) => x === optionId);
      if (optionIndex > -1) {
        this.selectedAttribute[index].selectedOptions.splice(optionIndex, 1);
      } else {
        this.selectedAttribute[index].selectedOptions.push(optionId);
      }
    } else {
      this.selectedAttribute.push({
        attributeCode: attrId,
        selectedOptions: [optionId],
      });
    }
    console.table(this.selectedAttribute);
  }
  isSelected(x: any): any {}
  openMenu(id: string): void {
    document.getElementById(id).click();
  }
  closeFilter(): void {
    $('body').removeClass('fix');
    $('.off-canvas-wrapper-mobile-filter').removeClass('open');
  }
  openFilter(): void {
    $('body').addClass('fix');
    $('.off-canvas-wrapper-mobile-filter').addClass('open');
  }

  emitEvent(): void {
    this.onFilterChange.emit(this.selectedAttribute);
    this.closeFilter();
  }
}
