import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { AddressdataService } from '../addressdata.service';
import { AddressType } from '../address';

@Component({
  selector: 'app-address-list',
  templateUrl: './address-list.component.html',
  styleUrls: ['./address-list.component.scss'],
})
export class AddressListComponent implements OnInit {
  constructor(
    private addressdataService: AddressdataService,
    private router: Router,
  ) {}

  addressList: AddressType[] = [];
  ngOnInit(): void {
    this.addressList = this.addressdataService.addressArray;
  }

  addAddress() {
    this.router.navigate(['add']);
  }

  onEdit(id: number) {
    this.addressdataService.getAdressInService(id);
    this.router.navigate(['edit', id]);
  }

  onDelete(id:number) {
    this.addressdataService.deleteDataInLocalStorage(id);
    this.ngOnInit();
  }
}