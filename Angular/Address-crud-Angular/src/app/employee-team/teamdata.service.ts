import { Injectable } from '@angular/core';
import { Team } from './team';
import { FormGroup } from '@angular/forms';

@Injectable({
  providedIn: 'root'
})

export class TeamdataService {
  constructor() { 
    this.teamArray = JSON.parse(localStorage.getItem('teamlist')) || [];
  }
  
  teamArray:Team[] = [];
  editObject:any= {};
  editForm:Team;
  
  addDataInLocalStorage(form: Team) {
     console.log("myForm", form);
     this.teamArray.push({
        id:this.teamArray.length + 1,
        teamName:form.teamName,
        employees:[...form.employees],
     });
     this.upDateInLocalStorage();
  } 

  getTeamDataInService(id:number) {
     const arr = this.teamArray.filter(ele => ele.id == id);
     this.editObject = {
        id:arr[0].id,
        teamName: arr[0].teamName,
        employees: arr[0].employees,
     }
  }

  deleteDataInLocalStorage(id:number) {
    this.teamArray = this.teamArray.filter(ele => ele.id != id);
    this.teamArray.forEach((ele, index) => {
      ele.id = index+1;
    })
    this.upDateInLocalStorage();
  }
  
  upDateInLocalStorage() {
    const lists = [];
    this.teamArray.forEach((ele) => {
      lists.push(ele);
    });
    localStorage.setItem('teamlist', JSON.stringify(lists));
  }
}
