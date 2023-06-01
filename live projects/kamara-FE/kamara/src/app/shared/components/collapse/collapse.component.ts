import { AfterViewInit, Component, Input, OnInit } from '@angular/core';
import { JsLoader } from '@shared/static/js-loader';

declare var $;

@Component({
  selector: 'app-collapse',
  templateUrl: './collapse.component.html',
  styleUrls: ['./collapse.component.scss'],
})
export class CollapseComponent implements OnInit, AfterViewInit {
  @Input() title = 'Collapsible Group Item #1';
  @Input() hasCustomHeader = false;
  @Input() open = false;

  isPlus = true;
  mainClass = `mainClass_${Math.floor(Math.random() * 10000000).toString()}`;
  constructor() {}

  ngOnInit(): void {
    this.$collapse.collapse('dispose');
    this.$collapse.collapse();
  }
  toggle(): void {
    this.isPlus = !this.isPlus;
    this.$collapse.collapse('toggle');
  }

  get $collapse(): any {
    return $('.' + this.mainClass);
  }

  ngAfterViewInit(): void {
    if (this.open) {
      this.toggle();
    }
  }
}
