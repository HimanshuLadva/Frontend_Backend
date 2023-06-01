import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.scss']
})
export class UserComponent implements OnInit {

  constructor() { }
  componentName:string = "Hello this is my user component";
  ngOnInit(): void {
  }
  
  sum(a:number,b:number) {
    return a+b;
  }
}
