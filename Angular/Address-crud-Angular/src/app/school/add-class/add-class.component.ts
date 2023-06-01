import { Component, OnInit } from '@angular/core';
import { FormArray, FormControl, FormGroup , Validators} from '@angular/forms';

@Component({
  selector: 'app-add-class',
  templateUrl: './add-class.component.html',
  styleUrls: ['./add-class.component.scss']
})
export class AddClassComponent implements OnInit {

  constructor() { }

  classForm:FormGroup;
  ngOnInit(): void {
    this.classForm = new FormGroup({
      className: new FormControl('',[Validators.required]),
      teacherName: new FormControl('',[Validators.required, Validators.pattern('^[a-zA-Z]*')]),
      students: new FormArray([
        new FormGroup({
          name: new FormControl(''),
          rollNo: new FormControl(''),
          address: new FormControl(''),
          phoneNos: new FormArray([
            new FormGroup({
              phoneNo: new FormControl('')
            })
          ]),
        })
      ])
    })
  }

  submitClass() {
    console.log(this.classForm.value);
  }

  addClass() {
    const control = <FormArray>this.classForm.controls['students'];
    control.push(
      new FormGroup({
        name: new FormControl(''),
          rollNo: new FormControl(''),
          address: new FormControl(''),
          phoneNos: new FormControl(''),
      })
   )
  }

  removeStudentDetail(index:number) {
    const control = <FormArray>this.classForm.controls['students'];
    control.removeAt(index);
  }

  studnents(): FormArray {
    return this.classForm.get("students") as FormArray
  }
  studentNumbers(empIndex:number) : FormArray {
    return this.studnents().at(empIndex).get("skills") as FormArray;
  }

}
