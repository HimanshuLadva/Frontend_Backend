import {Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges} from '@angular/core';
import {ChangeContext, Options} from '@angular-slider/ngx-slider';

@Component({
  selector: 'app-price-range',
  templateUrl: './price-range.component.html',
  styleUrls: ['./price-range.component.scss']
})
export class PriceRangeComponent implements OnInit, OnChanges {

  @Input() floor = 0;
  @Input() ceil = 100;
  @Output() priceChange = new EventEmitter<any>();

  priceMin = 10;
  priceMax = 50;
  // @Input() step = 5;
  options: Options = {
    floor: this.floor,
    ceil: this.ceil,
    // step: this.step,
    // showTicks: true
  };

  constructor() {
  }

  ngOnChanges(changes: SimpleChanges): void {
    this.options = {
      floor: this.floor,
      ceil: this.ceil,
    };
    this.priceMin = this.floor;
    this.priceMax = this.ceil;
  }

  ngOnInit(): void {
  }

  handleUserChangeEnd(event: ChangeContext): void {
    this.priceChange.emit({price_min: event.value, price_max: event.highValue});
  }
}
