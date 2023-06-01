import {Component, OnInit} from '@angular/core';
import {AuthService} from '@auth/service/auth.service';

@Component({
  selector: 'app-account-payment-method-tab',
  templateUrl: './account-payment-method-tab.component.html',
  styleUrls: ['./account-payment-method-tab.component.scss']
})
export class AccountPaymentMethodTabComponent implements OnInit {

  constructor(public authService: AuthService) {
  }

  ngOnInit(): void {
  }

}
