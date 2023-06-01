import { Component, OnInit , Input} from '@angular/core';

@Component({
  selector: 'app-user-details2',
  templateUrl: './user-details2.component.html',
  styleUrls: ['./user-details2.component.scss']
})
export class UserDetails2Component implements OnInit {

  constructor() { }
  @Input() item:{name:string, email:string} = {name:'', email:''};
  ngOnInit(): void {
  }

}
