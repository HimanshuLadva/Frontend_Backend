import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {SuperAttribute} from '@modals/product.modal';
import {Urls} from '@shared/config/url';

declare var $;

@Component({
  selector: 'app-super-attribute',
  templateUrl: './super-attribute.component.html',
  styleUrls: ['./super-attribute.component.scss']
})
export class SuperAttributeComponent implements OnInit {

  @Input() superAttribute: SuperAttribute;
  selectedValue: any;

  @Output() changeAttribute: EventEmitter<any> = new EventEmitter<any>();
  selectedColor;

  constructor() {
  }

  ngOnInit(): void {
  }


  onChangeSelect($event: any): void {
    this.changeAttribute.emit({value: $event.target.value, code: this.superAttribute.code});
  }

  onChangeColor(id): void {
    this.selectedColor = id;
    this.changeAttribute.emit({value: id, code: this.superAttribute.code});
  }

  onChangeImage(id): void {
    this.selectedColor = id;
    this.changeAttribute.emit({value: id, code: this.superAttribute.code});
  }

  getImageUrl(image): string {
    return `${Urls.baseMainUrl}cache/small/${image}`;
  }
}
