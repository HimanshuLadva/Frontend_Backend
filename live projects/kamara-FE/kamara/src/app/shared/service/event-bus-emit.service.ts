import { Injectable } from '@angular/core';
import { BusEvents } from './event-bus/bus-events';
import { EmitBusEvent } from './event-bus/emit-bus-event';
import { EventBusService } from './event-bus/event-bus.service';
import { Category } from '@modals/category.modal';

@Injectable({
  providedIn: 'root',
})
export class EventBusEmitService {
  constructor(private eventBus: EventBusService) {}

  showQuickViewModal(id: number): void {
    this.eventBus.emit(new EmitBusEvent(BusEvents.showQuickViewModal, id));
  }

  showLoading(): void {
    this.eventBus.emit(new EmitBusEvent(BusEvents.showLoading));
  }

  showFooter(): void {
    this.eventBus.emit(new EmitBusEvent(BusEvents.showFooter));
  }

  hideLoading(): void {
    this.eventBus.emit(new EmitBusEvent(BusEvents.hideLoading));
  }

  showMiniCartModal(): void {
    this.eventBus.emit(new EmitBusEvent(BusEvents.showMiniCartModal));
  }

  hideMiniCartModal(): void {
    this.eventBus.emit(new EmitBusEvent(BusEvents.hideMiniCart));
  }

  reloadWishlistCount(): void {
    this.eventBus.emit(new EmitBusEvent(BusEvents.reloadWishlistCount));
  }

  reloadCartCount(): void {
    this.eventBus.emit(new EmitBusEvent(BusEvents.reloadCartCount));
  }

  showMobileFilter(): void {
    this.eventBus.emit(new EmitBusEvent(BusEvents.showMobileFilter));
  }

  hideMobileFilter(): void {
    this.eventBus.emit(new EmitBusEvent(BusEvents.hideMobileFilter));
  }

  showMessage(data: any): void {
    this.eventBus.emit(new EmitBusEvent(BusEvents.showMessage, data));
  }

  productListCategoryBanner(data: any): void {
    this.eventBus.emit(
      new EmitBusEvent(BusEvents.productListCategoryBanner, data)
    );
  }
}
