import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { AccountComponent } from './account/account.component';
import { AuthRoutingModule } from './auth-routing.module';
import { SharedModule } from '@shared/shared.module';
import { ReactiveFormsModule } from '@angular/forms';
import { AccountAddressTabComponent } from './pages/account-address-tab/account-address-tab.component';
import { AccountDashboardTabComponent } from './pages/account-dashboard-tab/account-dashboard-tab.component';
import { AccountOrderTabComponent } from './pages/account-order-tab/account-order-tab.component';
import { AccountDetailTabComponent } from './pages/account-detail-tab/account-detail-tab.component';
import { AccountDownloadTabComponent } from './pages/account-download-tab/account-download-tab.component';
import { AccountPaymentMethodTabComponent } from './pages/account-payment-method-tab/account-payment-method-tab.component';

@NgModule({
  declarations: [
    LoginComponent,
    RegisterComponent,
    AccountComponent,
    AccountAddressTabComponent,
    AccountDashboardTabComponent,
    AccountOrderTabComponent,
    AccountDetailTabComponent,
    AccountDownloadTabComponent,
    AccountPaymentMethodTabComponent,
    RegisterComponent,
  ],
  imports: [CommonModule, AuthRoutingModule, SharedModule, ReactiveFormsModule],
})
export class AuthModule {}
