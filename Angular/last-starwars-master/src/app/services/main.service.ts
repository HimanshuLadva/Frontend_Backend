import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class MainService {

  PEOPLE_URL = 'https://swapi.dev/api/people';
  FILM_URL = 'https://swapi.dev/api/films';
  PLANET_URL = 'https://swapi.dev/api/planets';
  SPECIE_URL = 'https://swapi.dev/api/species';
  STARSHIP_URL = 'https://swapi.dev/api/starships';
  VEHICLE_URL = 'https://swapi.dev/api/vehicles';

  specifyData:any[]=[]
  
  constructor(private _http: HttpClient) { }

  getAllVehicle(page:number) {
     return this._http.get(this.VEHICLE_URL + '?page=' + page);
  }

  getAllStarship(page:number) {
     return this._http.get(this.STARSHIP_URL + '?page=' + page);
  }

  getAllSpecies(page:number) {
     return this._http.get(this.SPECIE_URL + '?page=' + page);
  }
 
  getAllPlanet(page:number) {
    return this._http.get(this.PLANET_URL + '?page=' + page);
  }

  getAllCherecter(page:number) {
     return this._http.get(this.PEOPLE_URL + '?page=' + page);
  }

  getAllFilm(page:number) {
    return this._http.get(this.FILM_URL + '?page=' + page);
  }
  
  async getHomeWorld(url:string) {
    await this._http.get(url).toPromise().then((res:any) => {
       return res;
    })
  }
  async getSpecifyData(url:string) {
    await this._http.get(url).toPromise().then((res:any) => {
      this.specifyData = res;
    });
    return this.specifyData;
  } 
}
