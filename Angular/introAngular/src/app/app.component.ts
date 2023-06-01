import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'myFirstApp Himanshu'; //property 
  data = 'Hello-world';
  num = 100;
  color = "blue"
  bgColor = "gray"
  updateColor() {
    this.color = "cyan";
    this.bgColor = "yellow"; 
  }
  // users:number [] = [1,2,3,4,5,6];
  // arr:any [] = [
  //   {name: 'himanshu', age: 21},  
  //   {name: 'yash', age: 22},
  //   {name: 'darshit', age: 23},
  //   {name: 'vishal', age: 24},
  // ]
  // nestedArr:{name:string,age:number,accounts?:string[]}[] = [
  //   {name: 'himanshu', age: 21, accounts:['facebook', 'gmail', 'google']},
  //   {name: 'yash', age: 22, accounts:['youtube', 'yahoo', 'google']},
  //   {name: 'darshit', age: 23, accounts:['instagram', 'skyup', 'google']},
  //   {name: 'vishal', age: 24, accounts:['sharechat', 'snapchat', 'google']},
  // ]
  // color = 'cyan';
  // disabled = false;
  // displayData = '';
  // count= 0;
  // show = true;

  greettings() {
     return "how are you";
  }

  // getName(value: string | number | boolean) {
  //    alert(value);
  // }
  // getData(value: any) {
  //   console.log(value); 
  // }
  // getValue(value: any) {
  //   this.displayData = value;
  //   console.log(this.displayData); 
  // }
  // counter(value: string) {
  //   value == 'inc'?this.count++:this.count--;
  // }

}
