import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {AbstractControl, FormBuilder, FormGroup, Validators} from '@angular/forms';
import {AddressDetail, Country} from '@modals/address.modal';
import {AuthService} from '@auth/service/auth.service';
import {CustomerService} from '@auth/service/customer.service';
import {EventBusEmitService} from '@shared/service/event-bus-emit.service';

@Component({
  selector: 'app-checkout-billing-form',
  templateUrl: './checkout-billing-form.component.html',
  styleUrls: ['./checkout-billing-form.component.scss']
})
export class CheckoutBillingFormComponent implements OnInit {


  @Input() isNew = false;
  @Output() onNewAddressSave: EventEmitter<AddressDetail> = new EventEmitter<AddressDetail>();

  addressForm: FormGroup;
  countryList: Country[];
  isAddressFormSubmitted = false;

  savedAddress: AddressDetail;
  savedAddressId;

  constructor(public authService: AuthService, private fb: FormBuilder, private customerService: CustomerService, private eventBusEmitService: EventBusEmitService) {
  }

  async ngOnInit(): Promise<void> {
    this.addressForm = this.getAddressForm();
    await this.loadAllCountry();
  }

  getAddressData(): { address?: any, isValid: boolean } {
    const res: { address?: any, isValid: boolean } = {isValid: false};
    if (!this.savedAddressId) {
      this.eventBusEmitService.showMessage({msg: 'Kindly add Shipping address'});
      return res;
    }
    res.address = {
      shipping: {
        address_id: this.savedAddressId,
      }
    };
    res.isValid = true;
    return res;
  }

  async loadAllCountry(): Promise<any> {
    try {
      const res = await this.customerService.getAllCountries();
      this.countryList = res.data;
    } catch (e) {
      console.error('::::AAA::::BBB::::', e);
    }
  }

  hasError(control: AbstractControl, isLogin = false): boolean {
    return !!(control && control.invalid && (control.touched || this.isAddressFormSubmitted));
  }

  async addressFormSubmit(): Promise<void> {
    this.isAddressFormSubmitted = true;
    if (this.addressForm.invalid) {
      return;
    }
    const data = this.mapperForAddress(this.addressForm.value);
    try {
      const res: any = await this.customerService.saveAddress(data);
      if (res) {
        this.savedAddress = res.data;
        this.savedAddressId = res.data.id;
        this.onNewAddressSave.emit(this.savedAddress);
      }
      if (res.error) {
        this.eventBusEmitService.showMessage({msg: res.error.message});
      } else {
        this.eventBusEmitService.showMessage({msg: res.message});
      }
    } catch (e) {
      console.error('::::AAA::::BBB::::', e);
    }
  }


  async loadFromPostcode(value: any): Promise<any> {
    try {
      const res = await this.customerService.getCityStateFromPostcode(value);
      if (res != 404) {
        console.clear();
        this.addressForm.get('state_').setValue(res.state);
        this.addressForm.get('city').setValue(res.district);
      }
    } catch (e) {
      console.error('::::AAA::::BBB::::', e);
    }
  }

  private getAddressForm(): FormGroup {
    return this.fb.group({
      id: [''],
      first_name: ['', [Validators.required]],
      last_name: ['', [Validators.required]],
      company_name: [''],
      // vat_id: [''],
      address1: [''],
      country: ['', [Validators.required]],
      country_name: ['India'],
      state_: ['', [Validators.required]],
      city: ['', [Validators.required]],
      postcode: ['', [Validators.required]],
      phone: [''],
      address_line_1: ['', [Validators.required]],
      address_line_2: [''],
    });
  }

  private mapperForAddress(obj: any): any {
    const data: AddressDetail = {address1: []};
    for (const key in obj) {
      if (obj[key] && key != 'address1') {
        if (key === 'state_') {
          data.state = obj[key];
        } else if (key === 'address_line_1' || key === 'address_line_2') {
          data.address1.push(obj[key]);
        } else if (key === 'country') {
          data[key] = obj[key];
          // data['country_name'] = this.countryList.find((c) => c.id == obj[key]).name;
        } else {
          data[key] = obj[key];
        }
      }
    }
    return data;
  }

}
