import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-peoplelist',
  templateUrl: './peoplelist.component.html',
  styleUrls: ['./peoplelist.component.scss'],
  
})
export class PeoplelistComponent implements OnInit {
  url = 'https://swapi.dev/api/people';
  tempUrl = '';
  pageCount: number = 1;
  details: { data: any; name: string; count: number }[] = [];
  printDetails: { data: any; name: string; count: number }[] = [];
  nextUrl: string = 'https://swapi.dev/api/people/?page=1';

  constructor(private http: HttpClient, private route: Router) {
    this.printDetails = this.details;
  }

  async ngOnInit() {
    while (true && this.nextUrl) {
      let tempUrl = `${this.url}/${'?page='}${this.pageCount}`;
      // tempUrl 
      this.pageCount++;

      await this.http
        .get<any>(tempUrl)
        .toPromise()
        .then((data) => {
          this.nextUrl = data.next;
          data.results.forEach((ele: any) => {
            let imageNum = Number(ele.url.match(/\d+/g).join(''));
            // let dataArr = data.results.filter(
            //   (element: any) => element.url.match(/\d+/g).join('') == imageNum
            // );
            this.details.push({
              data: ele,
              name: ele.name,
              count: imageNum,
            });
          });
        });
    }
  }
  peopleDetail(id: number) {
    this.route.navigate(['peoplelist', id]);
  }
  getingData(data:any) {
     console.log("data i ree",data) 
  }
  // printDetails = details;
}
