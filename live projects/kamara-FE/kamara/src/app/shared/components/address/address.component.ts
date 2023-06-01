import {Component, Input, OnInit} from '@angular/core';
import {AddressDetail} from '@modals/address.modal';

@Component({
  selector: 'app-address',
  templateUrl: './address.component.html',
  styleUrls: ['./address.component.scss']
})
export class AddressComponent implements OnInit {

  @Input() address: AddressDetail;

  constructor() {
  }

  ngOnInit(): void {
  }

}
