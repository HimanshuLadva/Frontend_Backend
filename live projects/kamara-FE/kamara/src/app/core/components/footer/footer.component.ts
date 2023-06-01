import { Component, OnDestroy, OnInit } from '@angular/core';
import { SubSink } from 'subsink';
import { BusEvents } from '@shared/service/event-bus/bus-events';
import { EventBusService } from '@shared/service/event-bus/event-bus.service';

@Component({
  selector: 'app-footer',
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.scss'],
})
export class FooterComponent implements OnInit, OnDestroy {
  subs = new SubSink();

  isLoading = true;
  constructor(private eventBus: EventBusService) {}

  async ngOnInit(): Promise<void> {
    await this.listenEventBus();
    setTimeout(() => {
      this.isLoading = false;
    }, 5000);
  }

  ngOnDestroy(): void {
    this.subs.unsubscribe();
  }

  async listenEventBus(): Promise<void> {
    this.subs.sink = this.eventBus.on(BusEvents.showFooter, () => {
      this.isLoading = false;
    });
  }
}
