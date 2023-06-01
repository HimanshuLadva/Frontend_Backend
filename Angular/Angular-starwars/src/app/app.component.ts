import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent {
  title = 'starwars';
  
  /* async ngOnInit() {
    const element = this.elementRef.nativeElement.querySelector('.main_div');
    for(let i = 1; i <= 9; i++) {
      let tempUrl = '';
      tempUrl = `${this.url}/${'?page='}${this.pageCount}`;
      this.pageCount++;
      
      this.http.get<any>(tempUrl).subscribe(
        (data) => {
        data.results.forEach((ele: any) => {
          let html = '';
          if(this.count != 17) {
            html = `
            <div class="detail">
               <img src="https://starwars-visualguide.com/assets/img/characters/${this.count}.jpg" alt="No image found">
               <h2>${ele.name}</h2>
            </div>
            `;
          }
          else {
            this.count++;
            html = `
            <div class="detail">
               <img src="https://starwars-visualguide.com/assets/img/characters/${this.count}.jpg" alt="No image found">
               <h2>${ele.name}</h2>
            </div>
            `;
          }
          element.insertAdjacentHTML('beforeend', html);
          this.count++;
        })
      }
      // (error) => {
      //   console.log(error.ok);
      //   this.ok = error.ok;
      // }
      );
      await this.http
        .get<any>(tempUrl)
        .toPromise()
        .then((data) => {
      });
      // if(!this.ok) {
      //   break;
      // }
    }
  } */

  // for(let i = 1; i <= 83; i++) {
  //   if(i !== 17) {
  //     this.tempUrl = `${this.url}/${i}${'.jpg'}`;
  //     this.images.push(this.tempUrl);
  //   }
  // }

  // constructor(private apidata: ApidataService) {
  //   // console.log('apidata', apidata);
  //   // for (let i = 1; i <= 9; i++) {
  //   //   apidata.peoples(i).subscribe((data: any) => {
  //   //     console.log("data",data.results);
  //   //     this.peopleM.push(data.results);
  //   //     console.log('i', i);
  //   //   });
  //   // }

  //   // this.temppeopleM = this.peopleM;

  //   // this.tempImages = this.images;
  // }

  // async await

  // async getData(url2: string) {
  //   let data = await this.apidata.getDataSynchronous(url2);
  //   console.log('data', data.results);
  //   this.peopleM.push(data.results);
  // }
  // ngOnInit(): void {
  //   for (let i = 1; i <= 9; i++) {
  //     this.tempUrl = `${this.url2}/${'?page='}${i}`;
  //     this.getData(this.tempUrl);
  //   }
  // }
}
