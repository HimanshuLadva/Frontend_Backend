import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-rating-stars',
  templateUrl: './rating-stars.component.html',
  styleUrls: ['./rating-stars.component.scss'],
})
export class RatingStarsComponent implements OnInit {
  @Input() rating = 0;

  constructor() {}

  ngOnInit(): void {}
}
