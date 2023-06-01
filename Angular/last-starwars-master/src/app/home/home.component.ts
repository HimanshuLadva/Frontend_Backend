import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  constructor() { }
  ngOnInit(): void {
    // localStorage.setItem('pagination', JSON.stringify({
    //   charecterPage:1,
    //   planetPage:1,
    //   speciePage:1,
    //   starshipPage:1,
    //   vehiclePage:1,
    // }))
    localStorage.setItem('charecterPage', JSON.stringify(1));
    localStorage.setItem('planetPage', JSON.stringify(1));
    localStorage.setItem('speciePage', JSON.stringify(1));
    localStorage.setItem('starshipPage', JSON.stringify(1));
    localStorage.setItem('vehiclePage', JSON.stringify(1));
  }

}
