import { Component, OnInit , OnDestroy} from '@angular/core';
import { Router,ActivatedRoute } from '@angular/router';
import { MainService } from 'src/app/services/main.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-listing',
  templateUrl: './listing.component.html',
  styleUrls: ['./listing.component.scss']
})
export class ListingComponent implements OnInit, OnDestroy { 
  title:string = '';
  chars:any;
  p:number = Number(localStorage.getItem('charecterPage')) || 1;
  total:number = 0;
  loading = true;
  subs:Subscription = new Subscription();
  slug:any;
  imageDetail:any;

  constructor(private _mainService: MainService, private _router: Router, private _route:ActivatedRoute) { }
  ngOnInit(): void {
    // console.log(this._route.snapshot.paramMap.get('slug'));
    this.slug = this._route.snapshot.paramMap.get('slug');    
    this.title = this.slug;
    if(this.slug == 'people') {
       this.imageDetail = 'characters';
    }
    else {
      this.imageDetail = this.slug;
    }
    this.selection();
  }

  selection() {
    switch (this.slug) {
      case 'people':
        this.getCherecters();
        break;
      case 'films':
        this.getFilms();
        break;
      case 'species':
        this.getSpecies();
        break;
      case 'starships':
        this.getStarships();
        break;
      case 'vehicles':
        this.getVehicles();
        break;
      case 'planets':
        this.getPlanets();
        break;
    }
  }
  getCherecters() {
    this.subs = this._mainService.getAllCherecter(this.p).subscribe((response:any) => {
       response.results.forEach((ele:any) => ele.url = Number(ele.url.match(/\d+/g).join('')))
       this.chars = response.results;
       this.total = response.count;
       this.loading = false;
    })
  }
  getSpecies() {
    this.subs = this._mainService.getAllSpecies(this.p).subscribe((response:any) => {
       response.results.forEach((ele:any) => ele.url = Number(ele.url.match(/\d+/g).join('')))
       this.chars = response.results;
       this.total = response.count;
       this.loading = false;
    })
  }
  getStarships() {
    this.subs = this._mainService.getAllStarship(this.p).subscribe((response:any) => {
       response.results.forEach((ele:any) => ele.url = Number(ele.url.match(/\d+/g).join('')))
       this.chars = response.results;
       this.total = response.count;
       this.loading = false;
    })
  }
  getVehicles() {
    this.subs = this._mainService.getAllVehicle(this.p).subscribe((response:any) => {
       response.results.forEach((ele:any) => ele.url = Number(ele.url.match(/\d+/g).join('')))
       this.chars = response.results;
       this.total = response.count;
       this.loading = false;
    })
  }
  getPlanets() {
    this.subs = this._mainService.getAllPlanet(this.p).subscribe((response:any) => {
       response.results.forEach((ele:any) => ele.url = Number(ele.url.match(/\d+/g).join('')))
       this.chars = response.results;
       this.total = response.count;
       this.loading = false;
    })
  }
  getFilms() {
    this.subs = this._mainService.getAllFilm(this.p).subscribe((response:any) => {
       response.results.forEach((ele:any) => ele.url = Number(ele.url.match(/\d+/g).join('')))
       this.chars = response.results;
       this.total = response.count;
       this.loading = false;
    })
  }

  pageChangeEvent(event: number) {
    this.p = event;
    localStorage.setItem('charecterPage', JSON.stringify(this.p));
    this.loading = true;
    // this.getCherecters();
    this.selection();
  }
  moveToHome() {
    this._router.navigate(['']);
  }

  ngOnDestroy(): void {
     this.subs.unsubscribe();
  }

}
