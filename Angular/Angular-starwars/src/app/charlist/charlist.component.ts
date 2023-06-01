import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-charlist',
  templateUrl: './charlist.component.html',
  styleUrls: ['./charlist.component.scss'],
})
export class CharlistComponent implements OnInit {
  url = 'https://swapi.dev/api/people';
  tempUrl = '';
  pageCount: number = 1;
  details: { data: any; name: string; count: number }[] = [];
  printDetails: { data: any; name: string; count: number }[] = [];
  nextUrl: string = 'https://swapi.dev/api/people/?page=1';
  move: boolean = false;
  value:number = 12;

  constructor(private http: HttpClient) {}

  async ngOnInit() {
    while (true && this.nextUrl) {
      let tempUrl = '';
      tempUrl = `${this.url}/${'?page='}${this.pageCount}`;
      this.pageCount++;

      await this.http
        .get<any>(tempUrl)
        .toPromise()
        .then((data) => {
          this.nextUrl = data.next;
          data.results.forEach((ele: any) => {
            let imageNum = Number(ele.url.match(/\d+/g).join(''));
            let dataArr = data.results.filter(
              (element: any) => element.url.match(/\d+/g).join('') == imageNum
            );
            this.details.push({
              data: dataArr,
              name: ele.name,
              count: imageNum,
            });
          });
        });
    }
    this.printDetails = this.details;
    this.printDetails.forEach((ele: any) => {
      console.log(ele);
    });
  }
}
