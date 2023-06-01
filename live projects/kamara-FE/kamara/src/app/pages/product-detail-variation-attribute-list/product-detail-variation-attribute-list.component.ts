import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import {
  Product,
  SelectedSuperAttribute,
  SuperAttribute,
} from '@modals/product.modal';
import { AuthService } from '@auth/service/auth.service';

@Component({
  selector: 'app-product-detail-variation-attribute-list',
  templateUrl: './product-detail-variation-attribute-list.component.html',
  styleUrls: ['./product-detail-variation-attribute-list.component.scss'],
})
export class ProductDetailVariationAttributeListComponent implements OnInit {
  @Input() attributes: SuperAttribute[] = [];
  @Input() currentProduct: Product;
  @Output() onAttributeChange: EventEmitter<SelectedSuperAttribute[]> =
    new EventEmitter();

  selectedAttributes: SelectedSuperAttribute[] = [];

  constructor(public auth: AuthService) {}

  ngOnInit(): void {
  }

  handleAttributeChange($event: SelectedSuperAttribute): void {
    const index = this.selectedAttributes?.findIndex(
      (d) => d.code == $event.code
    );
    if (index > -1) {
      this.selectedAttributes[index].value = $event.value;
    } else {
      if (this.selectedAttributes?.length > 0) {
        this.selectedAttributes.push($event);
      } else {
        this.selectedAttributes = [$event];
      }
    }
    if (this.selectedAttributes?.length > 0) {
      this.onAttributeChange.emit(this.selectedAttributes);
    }
  }

  isOptionChecked(code, value): boolean {
    try {
      return this.currentProduct[code] == value;
    } catch (e) {
      // console.error('::::AAA::::BBB::::', e);
    }
    return false;
    // return !!this.selectedAttributes.find((x) => {
    //   return x.code == code && x.value == value;
    // });
  }
  getSelectedName(attr: SuperAttribute): string {
    try {
      const code = this.currentProduct[attr.code];
      if (code) {
        const selectedLabel = attr?.options?.find((x) => x.id == code).label;
        return `(${selectedLabel})`;
      }
    } catch (e) {}
    return '';
  }
}
