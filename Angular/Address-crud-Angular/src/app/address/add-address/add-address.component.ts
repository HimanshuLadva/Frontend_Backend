import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AddressdataService } from '../addressdata.service';

@Component({
  selector: 'app-add-address',
  templateUrl: './add-address.component.html',
  styleUrls: ['./add-address.component.scss']
})
export class AddAddressComponent implements OnInit {

  constructor(private router: Router, private addressdataService :AddressdataService) { }

  form:FormGroup;
  
  
  ngOnInit(): void{

     this.form = new FormGroup({
        fname: new FormControl("", [Validators.required,Validators.pattern('[a-zA-Z]*')]),
        lname: new FormControl('', [Validators.required,Validators.pattern('[a-zA-Z]*')]),
        companyName: new FormControl('', [Validators.required]),
        country :new FormControl('', [Validators.required,Validators.pattern('[a-zA-Z]*')]),
        streetAdress:new FormControl('', [Validators.required]),
        postcode:new FormControl('', [Validators.required, Validators.minLength(6)]),
        town:new FormControl('', [Validators.required,Validators.pattern('[a-zA-Z]*')]),
        state:new FormControl('', [Validators.required,Validators.pattern('[a-zA-Z]*')]),
        phone:new FormControl('', [Validators.required, Validators.minLength(13),Validators.pattern("^[0-9]*")]),
     })
  }

  submitData() {
    this.addressdataService.setAddressData(this.form.value);
    this.router.navigate(['/addresslist']);
  }

  get fname() {
    return this.form.get('fname');
  } 
  get lname() {
    return this.form.get('lname');
  } 
  get companyName() {
    return this.form.get('companyName');
  } 
  get country() {
    return this.form.get('country');
  } 
  get streetAdress() {
    return this.form.get('streetAdress');
  } 
  get postcode() {
    return this.form.get('postcode');
  } 
  get town() {
    return this.form.get('town');
  } 
  get state() {
    return this.form.get('state');
  } 
  get phone() {
    return this.form.get('phone');
  } 
  
  moveToAddressList() {
    this.router.navigate(['addresslist']);
  }
}
