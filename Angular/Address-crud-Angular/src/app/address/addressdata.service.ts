import { Injectable } from '@angular/core';
import { AddressType } from './address';

@Injectable({
  providedIn: 'root'
})
export class AddressdataService  {

  constructor() {
    this.addressArray = JSON.parse(localStorage.getItem('AddressData')) || [];
  }
  
  addressArray:AddressType[] = [];
  editObject:any = {};

  setAddressData(form: AddressType) {
    this.addressArray.push({
      id:this.addressArray.length + 1,                              
      fname: form.fname,
      lname: form.lname,
      companyName: form.companyName,
      country :form.country,
      streetAdress:form.streetAdress, 
      postcode:form.postcode,
      town:form.town,
      state:form.state,
      phone:form.phone,
    });
    const lists = []; 
    this.addressArray.forEach(ele => {
      lists.push(ele);
    })
    localStorage.setItem('AddressData', JSON.stringify(lists));
  }

  getAdressInService(id:number) {
     const arr = this.addressArray.filter((ele) => ele.id === id);
     this.editObject = {
      id:arr[0].id,
      fname:arr[0].fname,
      lname:arr[0].lname,
      companyName: arr[0].companyName,
      country:arr[0].country,
      streetAdress: arr[0].streetAdress,
      postcode:arr[0].postcode,
      town:arr[0].town,
      state:arr[0].state,
      phone:arr[0].phone
     } 
  }

  editDataInLocalStorage(id:number, editFormValue: AddressType) {
   const index = this.addressArray.findIndex((ele) => ele.id === id);   
   this.addressArray[index] = {id, ...editFormValue};
   this.upDateLocalStorage();
  } 

  deleteDataInLocalStorage(id:number) {
    this.addressArray = this.addressArray.filter((ele) => ele.id !== id);
    this.addressArray.forEach((ele, index) => {
      ele.id = index+1;
    });
    this.upDateLocalStorage();
  }

  upDateLocalStorage() {
    const lists = [];
    this.addressArray.forEach((ele) => {
      lists.push(ele);
    });
    localStorage.setItem('AddressData', JSON.stringify(lists));
  }
  // getAddressData() {
  //   return JSON.parse(localStorage.getItem('AddressData'));
  // }
}
