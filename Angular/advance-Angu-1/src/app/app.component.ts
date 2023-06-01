import { Component } from '@angular/core';
import { UserdataService } from './services/userdata.service';
import { ApiDataService } from './services/api-data.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'advanceAngu';
  userDetail:any;
  users:any;

  constructor(private userdata: UserdataService, private apiData: ApiDataService) {
      console.log("userData", userdata.users());
      this.userDetail = userdata.users();
      // api -2
      apiData.users().subscribe((data:any)=> {
        console.log(data);
        this.users = data;
      })
  }
  getUserData(item: string) {
     console.log("item",item); 
     this.apiData.saveUser(item).subscribe((result) => {
      console.log("new", result);
      
     });
  }
}
