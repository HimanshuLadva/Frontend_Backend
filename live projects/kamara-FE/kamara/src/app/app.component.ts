import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { BusEvents } from '@shared/service/event-bus/bus-events';
import { EventBusService } from '@shared/service/event-bus/event-bus.service';
import { SubSink } from 'subsink';
import {
  NavigationEnd,
  NavigationError,
  NavigationStart,
  Router,
} from '@angular/router';
import {
  AbstractControl,
  FormBuilder,
  FormGroup,
  Validators,
} from '@angular/forms';
import { Title , Meta} from '@angular/platform-browser';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit, OnDestroy {
  subs = new SubSink();
  @ViewChild('launchModel') launchModel;
  pincodeForm: FormGroup;

  isLoading = false;
  isShowMessage = false;
  messageType;
  message;
  pincode;

  constructor(
    private eventBus: EventBusService,
    private router: Router,
    private fb: FormBuilder,
    private title: Title,
    private meta: Meta
  ) {
    this.router.events.subscribe((event: any) => {
      if (event instanceof NavigationStart) {
        this.isLoading = true;
      } else if (event instanceof NavigationEnd) {
        window.scrollTo(0, 0);
        this.isLoading = false;
      } else if (event instanceof NavigationError) {
        window.scrollTo(0, 0);
        this.isLoading = false;
      }
    });
  }

  async ngOnInit(): Promise<void> {
    await this.listenEventBus();
    await this.isPincodeAvailable();
    await this.pincodeFormSetUp();
    this.title.setTitle("Online Shopping Site for Necklace, Rings, Braclets, Accessories, Diamond, Gemstone More. Best Offers!")
    this.meta.addTags([
      {
        name:'description',
        content:'Kamara specializes in selling high quality personalized jewelry, made just for you. Create the perfect gift or personalize something for yourself. Enjoy Free Shipping, 99 Day Returns and a Bonus Free Gift!'
      },
      {
        name:'keywords',
        content:"fine jewelry, personalized jewelry, mothers rings, mothers ring, mother's ring, mother's rings, birthstone jewelry mothers birthstone jewelry, family jewelry"
      }
    ]);
  }

  pincodeFormSetUp() {
    this.pincodeForm = this.fb.group({
      pincode: [
        '',
        [Validators.required, Validators.min(100000), Validators.max(999999)],
      ],
    });
  }

  async isPincodeAvailable() {
    this.pincode = await localStorage.getItem('pincode');
    if (!this.pincode) {
      this.launchModel.nativeElement.click();
    }
  }

  hasErrorClass(control: AbstractControl): string {
    if (this.hasError(control)) {
      return 'is-invalid';
    }
    return '';
  }

  hasError(control: AbstractControl): boolean {
    return !!(control && control.invalid && control.touched);
  }

  async addPincode(form: FormGroup) {
    if (form.valid) {
      let pincode = form.value.pincode;
      await localStorage.setItem('pincode', pincode);
      this.launchModel.nativeElement.click();
    }
  }

  ngOnDestroy(): void {
    this.subs.unsubscribe();
  }

  async listenEventBus(): Promise<void> {
    this.subs.sink = this.eventBus.on(BusEvents.showLoading, () => {
      this.isLoading = true;
    });
    this.subs.sink = this.eventBus.on(BusEvents.hideLoading, () => {
      this.isLoading = false;
    });
    this.subs.sink = this.eventBus.on(
      BusEvents.showMessage,
      ({ msg, type = null, time = 2000 }) => {
        this.message = msg;
        this.isShowMessage = true;
        this.messageType = type;
        setTimeout(() => {
          this.isShowMessage = false;
        }, time);
      }
    );
  }
}
