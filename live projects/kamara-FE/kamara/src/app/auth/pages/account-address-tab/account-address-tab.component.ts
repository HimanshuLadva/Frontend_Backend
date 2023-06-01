import {Component, ElementRef, OnInit, ViewChild} from '@angular/core';
import {AuthService} from '@auth/service/auth.service';
import {AbstractControl, FormBuilder, FormGroup, Validators} from '@angular/forms';
import {AddressDetail, Country} from '@modals/address.modal';
import {CustomerService} from '@auth/service/customer.service';
import {EventBusEmitService} from '@shared/service/event-bus-emit.service';

@Component({
  selector: 'app-account-address-tab',
  templateUrl: './account-address-tab.component.html',
  styleUrls: ['./account-address-tab.component.scss']
})
export class AccountAddressTabComponent implements OnInit {

  addressForm: FormGroup;
  addressList: AddressDetail[];
  countryList: Country[];
  @ViewChild('formButton', {static: false}) formButton: ElementRef;
  isFormOpen = false;
  isAddressFormSubmitted = false;

  constructor(public authService: AuthService, private fb: FormBuilder, private customerService: CustomerService, private eventBusEmitService: EventBusEmitService) {
  }


  async ngOnInit(): Promise<void> {
    this.addressForm = this.getAddressForm();
    await this.loadAllAddress();
    await this.loadAllCountry();
  }

  async loadAllCountry(): Promise<any> {
    try {
      const res = await this.customerService.getAllCountries();
      this.countryList = res.data;
    } catch (e) {
      console.error('::::AAA::::BBB::::', e);
    }
  }

  async loadAllAddress(): Promise<any> {
    try {
      const res = await this.customerService.getAllAddress();
      this.addressList = res.data;
    } catch (e) {
      console.error('::::AAA::::BBB::::', e);
    }
  }

  resetForm(): void {
    this.addressForm.reset();
    this.isAddressFormSubmitted = false;
  }

  hasErrorClass(control: AbstractControl, isLogin = false): string {
    if (this.hasError(control, isLogin)) {
      return 'is-invalid';
    }
    return '';
  }

  hasError(control: AbstractControl, isLogin = false): boolean {
    return !!(control && control.invalid && (control.touched || this.isAddressFormSubmitted));
  }

  clickFormButton(needReset = false): void {
    this.formButton.nativeElement.click();
    this.isFormOpen = !this.isFormOpen;
    if (needReset) {
      this.resetForm();
    }
  }

  async addressFormSubmit(): Promise<void> {
    this.isAddressFormSubmitted = true;
    if (this.addressForm.invalid) {
      return;
    }
    const data = this.mapperForAddress(this.addressForm.value);
    if (this.addressForm.value?.id) {
      try {
        const id = data.id;
        data.id = undefined;
        const res: any = await this.customerService.updateAddress(data, id);
        if (res.message) {
          this.addressList[this.addressList.findIndex(d => d.id == id)] = res.data;
          this.clickFormButton();
        }
        if (res.error) {
          this.eventBusEmitService.showMessage({msg: res.error.message});
        } else {
          this.eventBusEmitService.showMessage({msg: res.message});
        }
      } catch (e) {
        console.error('::::AAA::::BBB::::', e);
      }

    } else {
      try {
        const res: any = await this.customerService.saveAddress(data);
        if (res) {
          this.addressList.push(res.data);
          this.clickFormButton();
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
  }

  toggleForm(index = null): void {
    if (index == null) {
      this.resetForm();
    } else if (index >= 0) {
      this.loadFormData(index);
    }
    if (!this.isFormOpen) {
      this.clickFormButton();
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

  private loadFormData(index: number): void {
    const address = this.addressList[index];
    const data: AddressDetail = address;
    data['state_'] = data?.state;
    data['address_line_1'] = data.address1?.length > 0 ? data.address1[0] : '';
    data['address_line_2'] = data.address1?.length > 1 ? data.address1[1] : '';
    this.addressForm.reset(data);
  }
}
