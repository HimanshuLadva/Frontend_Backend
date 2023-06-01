import { Component, OnInit} from '@angular/core';
import { ActivatedRoute} from '@angular/router';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-peopledetail',
  templateUrl: './peopledetail.component.html',
  styleUrls: ['./peopledetail.component.scss'],
})
export class PeopledetailComponent implements OnInit {
  id = 0;
  url = 'https://swapi.dev/api/people';
  url2 = `https://swapi.dev/api/planets`;
  url3 = `https://swapi.dev/api/films`;
  url4 = `https://swapi.dev/api/species`;
  dataArr: any = '';
  species: string = '';
  film: string = '';
  homoworld: string = '';

  constructor(private active: ActivatedRoute, private http: HttpClient) {
    this.id = this.active.snapshot.params['id'];
    // let tempUrl = `${this.url}/${this.id}`;
    this.http.get(`${this.url}/${this.id}`).subscribe((data: any) => {
      this.dataArr = data;
    });
    let tempUrl2 = `${this.url2}/${this.id}`;
    this.http.get(tempUrl2).subscribe((data: any) => {
      this.homoworld = data.name;
    });
    // let tempUrl3 = `${this.url3}/${this.id}`;
    // this.http.get(tempUrl3).subscribe((data: any) => {
    //   console.log("Himanshu", data['title']);
    //   this.film = data['title'];
    // });
    let tempUrl4 = `${this.url4}/${this.id}`;
    this.http.get(tempUrl4).subscribe((data: any) => {
      this.species = data['name'];
    },
     (error:any) => {
        console.log(error);  
        this.species = "unknown"
     }
    );
  }

  ngOnInit() {}
}
