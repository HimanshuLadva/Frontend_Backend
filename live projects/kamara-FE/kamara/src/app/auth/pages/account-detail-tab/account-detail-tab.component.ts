import { Component, OnInit } from '@angular/core';
import { AuthService } from '@auth/service/auth.service';
import {
  AbstractControl,
  FormBuilder,
  FormGroup,
  Validators,
} from '@angular/forms';
import { CustomerService } from '@auth/service/customer.service';
import { EventBusEmitService } from '@shared/service/event-bus-emit.service';

@Component({
  selector: 'app-account-detail-tab',
  templateUrl: './account-detail-tab.component.html',
  styleUrls: ['./account-detail-tab.component.scss'],
})
export class AccountDetailTabComponent implements OnInit {
  detailForm: FormGroup;
  passwordResetForm: FormGroup;

  isDetailFormSubmitted = false;
  isPasswordResetFormSubmitted = false;

  constructor(
    public authService: AuthService,
    private fb: FormBuilder,
    private customerService: CustomerService,
    private eventBusEmitService: EventBusEmitService
  ) {}

  ngOnInit(): void {
    this.detailForm = this.getDetailForm();
    this.passwordResetForm = this.getPasswordResetForm();
    this.setForm();
  }

  hasErrorClass(control: AbstractControl, isPassword = false): string {
    if (this.hasError(control, isPassword)) {
      return 'is-invalid';
    }
    return '';
  }

  hasError(control: AbstractControl, isPassword = false): boolean {
    return !!(
      control &&
      control.invalid &&
      (control.touched ||
        (isPassword && this.isPasswordResetFormSubmitted) ||
        (!isPassword && this.isDetailFormSubmitted))
    );
  }

  async detailFormSubmit(): Promise<void> {
    this.isDetailFormSubmitted = true;
    if (this.detailForm.invalid) {
      return;
    }
    try {
      const res = await this.customerService.updateProfile(
        this.detailForm.value
      );
      if (res) {
        this.authService.updateUser(res.data);
      }
      if (res.error) {
        this.eventBusEmitService.showMessage({ msg: res.error.message });
      } else {
        this.eventBusEmitService.showMessage({ msg: res.message });
      }
    } catch (e) {
      console.error('::::AAA::::BBB::::', e);
    }
  }

  async passwordResetFormSubmit(): Promise<void> {
    this.isPasswordResetFormSubmitted = true;
    if (this.passwordResetForm.invalid) {
      return;
    }
    try {
      const res = await this.customerService.updatePassword(
        this.passwordResetForm.value
      );
      // if (res) {
      //   this.authService.updateUser(res.data);
      // }
      if (res.error) {
        this.eventBusEmitService.showMessage({ msg: res.error.message });
      } else {
        this.eventBusEmitService.showMessage({ msg: res.message });
      }
    } catch (e) {
      console.error('::::AAA::::passwordResetFormSubmit::::', e);
    }
  }

  private setForm(): void {
    this.detailForm.reset({
      first_name: this.authService.User.first_name,
      last_name: this.authService.User.last_name,
      gender: this.authService.User.gender,
      date_of_birth: this.authService.User.date_of_birth,
    });
  }

  private getPasswordResetForm(): FormGroup {
    return this.fb.group({
      old_password: ['', [Validators.required]],
      new_password: ['', [Validators.required]],
      confirm_new_password: ['', [Validators.required]],
    });
  }

  private getDetailForm(): FormGroup {
    return this.fb.group({
      first_name: ['', [Validators.required]],
      last_name: ['', [Validators.required]],
      gender: [''],
      date_of_birth: [''],
    });
  }
}
