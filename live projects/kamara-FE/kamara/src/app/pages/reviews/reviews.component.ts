import { Component, OnInit, ViewChild } from '@angular/core';
import { JsLoader } from '@shared/static/js-loader';

@Component({
  selector: 'app-reviews',
  templateUrl: './reviews.component.html',
  styleUrls: ['./reviews.component.scss']
})
export class ReviewsComponent implements OnInit {

  constructor() { }
  @ViewChild('refObj') ref;

  ngOnInit(): void {
    setTimeout(() => {
      JsLoader.loadReviewJs(this.ref?.nativeElement);
    }, 10);
  }

}
