import { Component } from '@angular/core';
import { UserdataService } from '../services/userdata.service';
import { ApiDataService } from '../services/api-data.service';


@Component({
  selector: 'app-footer',
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.scss']
})
export class FooterComponent {

  userDetail:any;
  users:any;
  constructor(private userdata: UserdataService, private apiData: ApiDataService) {
    //  api -1
     this.userDetail = userdata.users();
    //  api -2
    apiData.users().subscribe((data) => {
       this.users = data;
    })
  }
}
