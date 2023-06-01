import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-not-found',
  templateUrl: './not-found.component.html',
  styleUrls: ['./not-found.component.scss'],
})
export class NotFoundComponent implements OnInit {
  @Input('message') message =
    "Sorry, we can't find the page you're looking for";
  @Input('iconClass') iconClass = 'fa fa-frown-o fa-5x';
  constructor() {}

  ngOnInit(): void {}
}
