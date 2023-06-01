import { Component } from '@angular/core';
import { dataType } from './datatype';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'modelPrac';

  getData() {
    const data:dataType = {
      name: "himanshu",
      lname: "ladva",
      age: 21,
      address: "shapar-veraval",
    }
    return data;
  }
}
