import { Component, OnInit } from '@angular/core';
import {
  AbstractControl,
  FormBuilder,
  FormGroup,
  Validators,
} from '@angular/forms';
import { AuthService } from '@auth/service/auth.service';
import { ActivatedRoute, Router } from '@angular/router';
import { RouteConfig } from '@shared/config/route-config';
import { CartService } from '@services/cart.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'],
})
export class RegisterComponent implements OnInit {
  routeConfig = RouteConfig;
  loginForm: FormGroup;
  registerForm: FormGroup;
  registerFormSubmitted = false;
  loginFormSubmitted = false;
  registerFormError = null;
  loginFormError = null;
  returnUrl = null;

  constructor(
    private fb: FormBuilder,
    public authService: AuthService,
    private router: Router,
    private route: ActivatedRoute,
    private cartService: CartService
  ) {}

  async ngOnInit(): Promise<void> {
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'];
    if (this.authService.isUserLogin) {
      if (this.returnUrl) {
        await this.router.navigate([this.returnUrl]);
      } else {
        await this.router.navigate([RouteConfig.customer]);
      }
    }
    this.setupForms();
  }

  setupForms(): void {
    this.registerForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      first_name: ['', [Validators.required]],
      last_name: ['', [Validators.required]],
      password: ['', [Validators.required]],
      password_confirmation: ['', [Validators.required]],
    });
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required]],
    });
  }

  hasErrorClass(control: AbstractControl, isLogin = false): string {
    if (this.hasError(control, isLogin)) {
      return 'is-invalid';
    }
    return '';
  }

  hasError(control: AbstractControl, isLogin = false): boolean {
    return !!(
      control &&
      control.invalid &&
      (control.touched ||
        (isLogin && this.loginFormSubmitted) ||
        (!isLogin && this.registerFormSubmitted))
    );
  }

  async login(form: FormGroup): Promise<void> {
    this.loginFormSubmitted = true;
    if (form.invalid) {
      return;
    }
    this.loginFormError = [];
    try {
      const res = await this.authService.login(
        form.value.email,
        form.value.password
      );
      if (res) {
        // await this.cartService.seeForLoginUser();
        if (this.returnUrl) {
          await this.router.navigate([this.returnUrl]);
        } else {
          await this.router.navigate([RouteConfig.customer]);
        }
      }
    } catch (e) {
      this.loginFormError = AuthService.getErrorObj(e);
      // console.error('::::AAA::::BBB::::', e);
    }
  }

  async register(form: FormGroup): Promise<void> {
    this.registerFormSubmitted = true;
    if (form.invalid) {
      return;
    }
    this.registerFormError = [];
    try {
      let res = await this.authService.registerCore(form.value);
      if (res) {
        res = await this.authService.login(
          form.value.email,
          form.value.password
        );
        if (res) {
          if (this.returnUrl) {
            await this.router.navigate([this.returnUrl]);
          } else {
            await this.router.navigate([RouteConfig.customer]);
          }
        }
      }
    } catch (eaaa) {
      this.registerFormError = AuthService.getErrorObj(eaaa);
      // console.error('::::AAA::::BBB::::', e);
    }
  }
}
