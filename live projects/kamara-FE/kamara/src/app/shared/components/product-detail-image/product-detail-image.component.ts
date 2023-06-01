import {
  AfterContentChecked,
  AfterViewInit,
  ChangeDetectorRef,
  Component,
  Input,
  OnChanges,
  OnInit,
  QueryList,
  SimpleChanges,
  ViewChildren,
} from '@angular/core';
import { JsLoader } from '@shared/static/js-loader';
import { Image } from '@modals/product.modal';

@Component({
  selector: 'app-product-detail-image',
  templateUrl: './product-detail-image.component.html',
  styleUrls: ['./product-detail-image.component.scss'],
})
export class ProductDetailImageComponent
  implements OnInit, OnChanges, AfterViewInit
{
  isShowModal = false;
  @Input() productImage: Image[];
  @ViewChildren('desktopImgList') desktopImgList: QueryList<any>;
  @ViewChildren('mobileImgList') mobileImgList: QueryList<any>;
  constructor() {}

  ngOnInit(): void {}

  ngOnChanges(changes: SimpleChanges): void {}
  ngAfterViewInit(): void {
    this.desktopImgList.changes.subscribe((t) => {
      this.ngForRendered();
    });
    this.mobileImgList.changes.subscribe((t) => {
      this.ngForRendered();
    });
  }

  ngForRendered(): void {
    JsLoader.loadProductDetailJs();
    JsLoader.loadProductDetailPopUpJs();
  }

  toggleModal() {
    this.isShowModal = !this.isShowModal;
  }
}
