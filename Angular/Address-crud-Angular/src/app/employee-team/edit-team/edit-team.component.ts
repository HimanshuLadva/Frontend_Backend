import { Component, OnInit } from '@angular/core';
import { Team } from '../team';
import { TeamdataService } from '../teamdata.service';
import { FormArray, FormControl, Validators, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-edit-team',
  templateUrl: './edit-team.component.html',
  styleUrls: ['./edit-team.component.scss']
})
export class EditTeamComponent implements OnInit {

  constructor(private teamDataService: TeamdataService, private router: Router) { }
  
  editForm:FormGroup;

  dataGetFromTeamList:Team = {
    id:null,
    teamName:'',
    employees:[],
  }
  employeeArr:any[] =[];
  ngOnInit(): void {
    this.dataGetFromTeamList = this.teamDataService.editObject;
    this.employeeArr = this.teamDataService.editObject.employees;

    console.log("dataGetFromTeamList", this.teamDataService.editObject.employees);
    this.editForm = new FormGroup({
      teamName: new FormControl(''),
      employees: new FormArray([
        new FormGroup({
         employee: new FormControl('')
        })
      ])
   });
  }

  editData() {
    this.router.navigate(['teamemployee/view']);
  } 

  addEmployee() {
    const control = <FormArray>this.editForm.controls['employees'];
    control.push(
      new FormGroup({
        employee: new FormControl('')
      })
   )
  }

  removeEmployee(index:number) {
    const control = <FormArray>this.editForm.controls['employees'];
    control.removeAt(index);
  }
}
