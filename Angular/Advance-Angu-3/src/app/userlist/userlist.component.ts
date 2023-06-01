import { Component} from '@angular/core';

@Component({
  selector: 'app-userlist',
  templateUrl: './userlist.component.html',
  styleUrls: ['./userlist.component.scss']
})
export class UserlistComponent {

  constructor() { 
    console.log("hello lazy bhai");
    
  }

  ngOnInit(): void {
  }

}
