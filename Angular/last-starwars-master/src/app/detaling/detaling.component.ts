import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { Observable, Subscription } from 'rxjs';
import { map } from 'rxjs/operators';
import { MainService } from 'src/app/services/main.service';

@Component({
  selector: 'app-detaling',
  templateUrl: './detaling.component.html',
  styleUrls: ['./detaling.component.scss'],
})
export class DetalingComponent implements OnInit, OnDestroy {
  id: number = 0;
  detailArr: Observable<{}> = new Observable();
  data: any;
  imagePath: any;

  dataArr: { name: any; listDetail:any[]; loading: boolean }[] = [
    { name: '', listDetail: [], loading: true },
  ];
  detail: any[] = [];
  films: any[] = [];
  people: any[] = [];
  planets: any[] = [];
  vehicles: any[] = [];
  starships: any[] = [];
  species: any[] = [];

  // isFilms = false;
  // isPeople = false;
  // isPlanets = false;
  // isStarships = false;
  // isVehicles = false;
  // isSpecies = false;
  forBreadCrumb: string = '';

  homeworld: any;
  loading = true;
  loading1 = true;
  loading2 = true;
  loading3 = true;
  loading4 = true;
  loading5 = true;
  subs: Subscription = new Subscription();

  constructor(
    private _route: ActivatedRoute,
    private _http: HttpClient,
    private _router: Router,
    private _mainService: MainService
  ) {
    this._route.paramMap.subscribe((param) => {
      console.log('params', param.get('id'));
      this.loadData();
    });
  }

  ngOnInit(): void {}

  loadData(): void {
    this.imagePath = this._route.snapshot.paramMap.get('slug');
    this.forBreadCrumb = this.imagePath;
    console.log('this is', this._route.snapshot?.url[1]?.path);
    console.log('this is', this.imagePath);

    if (this.imagePath == 'people') this.imagePath = 'characters';
    this.id = Number(this._route.snapshot.paramMap.get('id'));

    this.allEmpty();
    this.detailArr = this._http
      .get(
        `https://swapi.dev/api/${this._route.snapshot?.url[1]?.path}/${this.id}`
      )
      .pipe(map((res) => res));
    this.subs = this.detailArr.subscribe((result) => {
      this.data = result;
      this.detail = Object.entries(this.data)
        .filter((ele) => typeof ele[1] != 'object')
        .filter((ele) => ele[0] != 'url');
      Object.entries(this.data).forEach((ele) => {
        console.log('ele', typeof ele[1]);
      });
      console.log('this dtaa', this.detail);

      switch (this._route.snapshot?.url[1]?.path) {
        case 'people':
          this._http.get(this.data.homeworld).subscribe((response: any) => {
            this.homeworld = response.name;
          });
          // this.dataArr = [];
          this.getVehicles(this.data.vehicles);
          this.getStarships(this.data.starships);
          this.getFilms(this.data.films);
          // this.isVehicles = true;
          // this.isStarships = true;
          // this.isFilms = true;
          // Object.keys(this.data).forEach((key) => {
          //   if(typeof this.data[key] == 'object' && key!='species') {
          //     console.log(`${key}: ${typeof this.data[key]}`);
          //     this.dataArr.push(key);
          //   }
          // })
          // console.log("this dtaa", this.data);

          break;
        case 'films':
          // this.dataArr = [];
          this.getPeople(this.data.characters);
          this.getPlanets(this.data.planets);
          this.getVehicles(this.data.vehicles);
          this.getStarships(this.data.starships);
          // this.getSpecies(this.data.species);
          // this.isVehicles = true;
          // this.isStarships = true;
          // this.isPlanets = true;
          // this.isPeople = true;
          // this.isSpecies = true;
          // Object.keys(this.data).forEach((key) => {
          //   if(typeof this.data[key] == 'object' && key!='species') {
          //     console.log(`${key}: ${typeof this.data[key]}`);
          //     this.dataArr.push(key);
          //   }
          // })
          break;
        case 'planets':
          // this.dataArr = [];
          this.getFilms(this.data.films);
          this.getPeople(this.data.residents);
          // this.isFilms = true;
          // this.isPeople = true;
          // Object.keys(this.data).forEach((key) => {
          //   if(typeof this.data[key] == 'object' && key!='species') {
          //     console.log(`${key}: ${typeof this.data[key]}`);
          //     this.dataArr.push(key);
          //   }
          // })
          break;
        case 'vehicles':
          // this.dataArr = [];
          this.getFilms(this.data.films);
          this.getPeople(this.data.pilots);
          // this.isFilms = true;
          // this.isPeople = true;
          // Object.keys(this.data).forEach((key) => {
          //   if(typeof this.data[key] == 'object' && key!='species') {
          //     console.log(`${key}: ${typeof this.data[key]}`);
          //     this.dataArr.push(key);
          //   }
          // })
          break;
        case 'starships':
          // this.dataArr = [];
          this.getFilms(this.data.films);
          this.getPeople(this.data.pilots);
          // this.isFilms = true;
          // this.isPeople = true;
          // Object.keys(this.data).forEach((key) => {
          //   if(typeof this.data[key] == 'object' && key!='species') {
          //     console.log(`${key}: ${typeof this.data[key]}`);
          //     this.dataArr.push(key);
          //   }
          // })
          break;
        case 'species':
          // this.dataArr = [];
          this.getFilms(this.data.films);
          this.getPeople(this.data.people);
          // this.isFilms = true;
          // this.isPeople = true;
          // Object.keys(this.data).forEach((key) => {
          //   if(typeof this.data[key] == 'object' && key!='species') {
          //     console.log(`${key}: ${typeof this.data[key]}`);
          //     this.dataArr.push(key);
          //   }
          // })
          break;
      }
    });
  }

  // func(item: string) {
  //   if (item == 'films') {
  //     return this.films;
  //   } else if (item == 'vehicles') {
  //     return this.vehicles;
  //   } else if (item == 'starships') {
  //     return this.starships;
  //   } else if (item == 'characters') {
  //     return this.people;
  //   } else if (item == 'planets') {
  //     return this.planets;
  //   }
  //   return this.films;
  // }

  // get films
  async getFilms(arr: any) {
    let i;
    this.films = [];
    for (i = 0; i < arr.length; i++) {
      const response = await this._mainService.getSpecifyData(arr[i]);
      this.films.push(response);
      this.films[this.films.length - 1].url = Number(
        this.films[this.films.length - 1].url.match(/\d+/g).join('')
      );
    }
    this.dataArr.push({
      name: 'films',
      listDetail: this.films,
      loading: false,
    });
    this.loading = false;
  }

  // get people
  async getPeople(arr: any) {
    let i;
    this.people = [];
    for (i = 0; i < arr.length; i++) {
      const response = await this._mainService.getSpecifyData(arr[i]);
      this.people.push(response);
      this.people[this.people.length - 1].url = Number(
        this.people[this.people.length - 1].url.match(/\d+/g).join('')
      );
    }
    this.dataArr.push({
      name: 'characters',
      listDetail: this.people,
      loading: false
    });
    this.loading1 = false;
  }

  // get planets
  async getPlanets(arr: any) {
    let i;
    this.planets = [];
    for (i = 0; i < arr.length; i++) {
      const response = await this._mainService.getSpecifyData(arr[i]);
      this.planets.push(response);
      this.planets[this.planets.length - 1].url = Number(
        this.planets[this.planets.length - 1].url.match(/\d+/g).join('')
      );
    }
    this.dataArr.push({
      name: 'planets',
      listDetail: this.planets,
      loading: false
    });
    this.loading2 = false;
  }

  // get vehicles
  async getVehicles(arr: any) {
    let i;
    this.vehicles = [];
    for (i = 0; i < arr.length; i++) {
      const response = await this._mainService.getSpecifyData(arr[i]);
      this.vehicles.push(response);
      this.vehicles[this.vehicles.length - 1].url = Number(
        this.vehicles[this.vehicles.length - 1].url.match(/\d+/g).join('')
      );
    }
    this.dataArr.push({
      name: 'vehicles',
      listDetail: this.vehicles,
      loading: false
    });
    this.loading3 = false;
  }

  // get starships
  async getStarships(arr: any) {
    let i;
    this.starships = [];
    for (i = 0; i < arr.length; i++) {
      const response = await this._mainService.getSpecifyData(arr[i]);
      this.starships.push(response);
      this.starships[this.starships.length - 1].url = Number(
        this.starships[this.starships.length - 1].url.match(/\d+/g).join('')
      );
    }
    this.dataArr.push({
      name: 'starships',
      listDetail: this.starships,
      loading: false
    });
    this.loading4 = false;
  }

  // get species
  async getSpecies(arr: any) {
    let i;
    this.species = [];
    for (i = 0; i < arr.length; i++) {
      const response = await this._mainService.getSpecifyData(arr[i]);
      this.species.push(response);
      this.species[this.species.length - 1].url = Number(
        this.species[this.species.length - 1].url.match(/\d+/g).join('')
      );
    }
    this.dataArr.push({
      name: 'species',
      listDetail: this.species,
      loading: false
    });
    this.loading5 = false; //loading change
  }

  allEmpty() {
    this.dataArr = [];
    // this.isFilms = this.isPeople = false;
    // this.isPlanets = false;
    // this.isVehicles = false;
    // this.isSpecies = false;
    // this.isStarships = false;
  }

  moveToDetail(id: number, item: string) {
    console.log('item', item);
    this.dataArr = [];
    const arr = ['pilots', 'residents', 'characters'];
    item = arr.includes(item) ? 'people' : item;
    this._router.navigate([`listof/${item}`, id]);
  }

  moveToHome() {
    this._router.navigate(['']);
  }

  moveToBack() {
    this._router.navigate([`listof/${this._route.snapshot?.url[1]?.path}`]);
  }

  ngOnDestroy(): void {
    this.subs.unsubscribe();
  }
}

// moveToFilm(id: number) {
//   this._router.navigate(['listof/films', id]);
// }
// moveToPeople(id: number) {
//   this._router.navigate(['listof/people', id]);
// }

// moveToPlanet(id: number) {
//   this._router.navigate(['listof/planets', id]);
// }

// moveToVehicles(id: number) {
//   this._router.navigate(['listof/vehicles', id]);
// }
// moveToStarships(id: number) {
//   this._router.navigate(['listof/starships', id]);
// }

// moveToSpecies(id: number) {
//   this._router.navigate(['listof/species', id]);
// }

// await this._http
//   .get<any>(arr[i])
//   .toPromise()
//   .then((data) => {

//     data.url = Number(data.url.match(/\d+/g).join(''));
//     _data.push(data);
//   });
