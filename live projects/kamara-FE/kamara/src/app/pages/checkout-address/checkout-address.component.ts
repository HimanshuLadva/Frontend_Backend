import {Component, ElementRef, OnInit, ViewChild} from '@angular/core';
import {AuthService} from '@auth/service/auth.service';
import {FormGroup} from '@angular/forms';
import {CustomerService} from '@auth/service/customer.service';
import {EventBusEmitService} from '@shared/service/event-bus-emit.service';
import {AddressDetail, Country} from '@modals/address.modal';
import {CheckoutBillingFormComponent} from '@pages/checkout-billing-form/checkout-billing-form.component';

@Component({
  selector: 'app-checkout-address',
  templateUrl: './checkout-address.component.html',
  styleUrls: ['./checkout-address.component.scss']
})
export class CheckoutAddressComponent implements OnInit {

  @ViewChild('click_here_to_select_address') click_here_to_select: ElementRef;
  @ViewChild('checkoutBillingFormComponent') checkoutBillingFormComponent: CheckoutBillingFormComponent;

  addressForm: FormGroup;
  addressList: AddressDetail[];
  countryList: Country[];
  isFormOpen = false;
  isAddressFormSubmitted = false;
  showAddressForm = false;
  selectedAddressId;
  selectedAddress: AddressDetail;
  showBillingForm = false;

  constructor(public authService: AuthService, private customerService: CustomerService, private eventBusEmitService: EventBusEmitService) {
  }

  async ngOnInit(): Promise<void> {
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

  getAddressData(): { address?: any, isValid: boolean } {
    const res: { address?: any, isValid: boolean } = {isValid: false};
    if (!this.selectedAddressId) {
      this.eventBusEmitService.showMessage({msg: 'Kindly Select Address'});
      return res;
    }
    let shipping = {};
    if (this.showBillingForm) {
      const _res = this.checkoutBillingFormComponent.getAddressData();
      if (!_res.isValid) {
        return;
      }
      shipping = _res.address?.shipping;
    }
    res.address = {
      billing: {
        address_id: this.selectedAddressId,
        use_for_shipping: !this.showBillingForm
      },
      shipping
    };
    res.isValid = true;
    return res;
  }

  addressSelect(addressId): void {
    this.selectedAddressId = addressId;
    this.click_here_to_select.nativeElement.click();
    this.selectedAddress = this.addressList.find(d => d.id == addressId);
    this.showAddressForm = false;
  }

  addAddressSelect(): void {
    this.click_here_to_select.nativeElement.click();
    this.showAddressForm = true;
    this.selectedAddressId = null;
    this.selectedAddress = null;
  }

  shipToDifferentAddressClicked($event: any): void {
    this.showBillingForm = $event.target.checked;
  }

  handleOnNewAddressSave($event: AddressDetail): void {
    this.addressList.push($event);
    this.selectedAddress = $event;
    this.selectedAddressId = $event.id;
    this.showAddressForm = false;
  }
}
