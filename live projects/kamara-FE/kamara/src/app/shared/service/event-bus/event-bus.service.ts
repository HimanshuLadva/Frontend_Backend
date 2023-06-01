import { Injectable } from '@angular/core';
import { Subject, Subscription } from 'rxjs';
import { filter, map } from 'rxjs/operators';
import { EmitBusEvent } from './emit-bus-event';
import { BusEvents } from './bus-events';

@Injectable({
  providedIn: 'root',
})
export class EventBusService {
  private subject$ = new Subject();

  emit(event: EmitBusEvent): any {
    this.subject$.next(event);
  }

  on(event: BusEvents, action: any): Subscription {
    return this.subject$
      .pipe(
        filter((e: any) => e.name === event),
        map((e: any) => e.value)
      )
      .subscribe(action);
  }
}
