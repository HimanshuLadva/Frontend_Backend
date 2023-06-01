import { Component, OnInit ,Input } from '@angular/core';

@Component({
  selector: 'app-inner-child',
  templateUrl: './inner-child.component.html',
  styleUrls: ['./inner-child.component.scss']
})
export class InnerChildComponent implements OnInit {

  constructor() { }
  @Input() inneritem = 0;
  ngOnInit(): void {
  }

}
