import { Component, OnInit } from '@angular/core';
import { FormArray, FormControl, Validators, FormGroup } from '@angular/forms';
import { TeamdataService } from '../teamdata.service';
import { Router } from '@angular/router';


@Component({
  selector: 'app-add-team',
  templateUrl: './add-team.component.html',
  styleUrls: ['./add-team.component.scss']
})
export class AddTeamComponent implements OnInit {

  constructor(private teamDataService: TeamdataService, private router: Router) { }
  
  teamForm:FormGroup;
  ngOnInit(): void {
    this.teamForm = new FormGroup({
       teamName: new FormControl('',[Validators.required, Validators.pattern('^[a-zA-Z]*')]),
       employees: new FormArray([
         new FormGroup({
          employee: new FormControl('', [Validators.required, Validators.pattern('^[a-zA-Z]*')])
         })
       ])
    });
  }
  
  get teamName() {
    return this.teamForm.get('teamName');
  }
  get employee():FormArray {
    return this.teamForm.get('employees') as FormArray;
  }

  addEmployee() {
    const control = <FormArray>this.teamForm.controls['employees'];
    control.push(
      new FormGroup({
        employee: new FormControl('')
      })
   )
  }

  removeEmployee(index:number) {
    const control = <FormArray>this.teamForm.controls['employees'];
    control.removeAt(index);
  }
  submitData() {
    console.log(this.teamForm.value);
    this.teamDataService.addDataInLocalStorage(this.teamForm.value);
    this.router.navigate(['teamemployee/view']);
  }
  
}
