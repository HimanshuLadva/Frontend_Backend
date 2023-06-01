import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { AddressdataService } from '../addressdata.service';
import { AddressType } from '../address';
import { Router } from '@angular/router';

@Component({
  selector: 'app-edit-address',
  templateUrl: './edit-address.component.html',
  styleUrls: ['./edit-address.component.scss'],
})
export class EditAddressComponent implements OnInit {
  constructor(
    private addressdataService: AddressdataService,
    private router: Router
  ) {}

  editForm: FormGroup;
  dataGetFromAddressList: AddressType = {
    id: null,
    fname: '',
    lname: '',
    companyName: '',
    country: '',
    streetAdress: '',
    postcode: null,
    town: '',
    state: '',
    phone: null,
  };
  ngOnInit(): void {
    this.dataGetFromAddressList = this.addressdataService.editObject;
    this.editForm = new FormGroup({
      fname: new FormControl("", [Validators.required,Validators.pattern('[a-zA-Z]*')]),
        lname: new FormControl('', [Validators.required,Validators.pattern('[a-zA-Z]*')]),
        companyName: new FormControl('', [Validators.required]),
        country :new FormControl('', [Validators.required,Validators.pattern('[a-zA-Z]*')]),
        streetAdress:new FormControl('', [Validators.required]),
        postcode:new FormControl('', [Validators.required, Validators.minLength(6)]),
        town:new FormControl('', [Validators.required,Validators.pattern('[a-zA-Z]*')]),
        state:new FormControl('', [Validators.required,Validators.pattern('[a-zA-Z]*')]),
        phone:new FormControl('', [Validators.required, Validators.minLength(13),Validators.pattern("^[0-9]*")]),
    });
  }

  get fname() {
    return this.editForm.get('fname');
  } 
  get lname() {
    return this.editForm.get('lname');
  } 
  get companyName() {
    return this.editForm.get('companyName');
  } 
  get country() {
    return this.editForm.get('country');
  } 
  get streetAdress() {
    return this.editForm.get('streetAdress');
  } 
  get postcode() {
    return this.editForm.get('postcode');
  } 
  get town() {
    return this.editForm.get('town');
  } 
  get state() {
    return this.editForm.get('state');
  } 
  get phone() {
    return this.editForm.get('phone');
  } 
  
  editData() {
    this.addressdataService.editDataInLocalStorage(
      this.dataGetFromAddressList.id,
      this.editForm.value
    );
    this.router.navigate(['addresslist']);
  }

  moveToAddressList() {
    this.router.navigate(['addresslist']);
  }
}
