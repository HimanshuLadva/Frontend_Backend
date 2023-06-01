import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {FilterableAttribute} from '@modals/filter.modal';
import {Static} from '@shared/static/static';

@Component({
  selector: 'app-filter-attribute',
  templateUrl: './filter-attribute.component.html',
  styleUrls: ['./filter-attribute.component.scss']
})
export class FilterAttributeComponent implements OnInit {
  @Input() attribute: FilterableAttribute;
  @Output() attributeOptionChange = new EventEmitter<any>();
  selectedOption = [];
  DEFAULT_SHOW_COUNT = Static.FILTER_ATTRIBUTE_SHOW_COUNT;
  showCount = this.DEFAULT_SHOW_COUNT;

  constructor() {
  }

  ngOnInit(): void {
  }

  handleAttributeClick(optionId): void {
    const index = this.selectedOption.findIndex(d => d == optionId);
    if (index > -1) {
      this.selectedOption.splice(index, 1);
    } else {
      this.selectedOption.push(optionId);
    }
    this.emitEvent();
  }

  showMore(isLess = false): void {
    if (isLess) {
      this.showCount = this.DEFAULT_SHOW_COUNT;
    } else {
      this.showCount =
        this.attribute?.options?.length > this.DEFAULT_SHOW_COUNT
          ? this.attribute?.options?.length
          : this.DEFAULT_SHOW_COUNT;
    }
  }

  isSelected(optionId): any {
    return this.selectedOption.find(x => x == optionId);
  }

  private emitEvent(): void {

    const obj = {code: this.attribute.code, value: this.selectedOption.toString()};
    this.attributeOptionChange.emit(obj);
  }
}

