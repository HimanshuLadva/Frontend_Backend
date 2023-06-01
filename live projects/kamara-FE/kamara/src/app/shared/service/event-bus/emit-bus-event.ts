import {BusEvents} from './bus-events';

export class EmitBusEvent {
  constructor(public name: BusEvents, public value: any = null) {
  }
}
