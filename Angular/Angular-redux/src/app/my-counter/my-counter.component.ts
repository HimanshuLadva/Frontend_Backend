import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Store } from '@ngrx/store';
import { increment, decrement, reset } from '../counter.action';

@Component({
  selector: 'app-my-counter',
  templateUrl: './my-counter.component.html',
  styleUrls: ['./my-counter.component.scss']
})
export class MyCounterComponent implements OnInit {
  
  count$: Observable<number>;
  constructor(private store: Store<{count: number}>) { 
    this.count$ = store.select('count');
  }

  ngOnInit(): void {
  }

  increment() {
     this.store.dispatch(increment());
  }

  decrement() {
     this.store.dispatch(decrement());
  }

  reset() {
    this.store.dispatch(reset());
  }
}
