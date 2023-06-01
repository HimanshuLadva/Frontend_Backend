import { InnerChildComponent } from './child/inner-child/inner-child.component';
import { Directive, ElementRef } from '@angular/core';

@Directive({
  selector: '[appChangeEl]'
})
export class ChangeElDirective {

  constructor(el: ElementRef) {
 
     el.nativeElement.innerHTML = "Hello Himanshu ";
  }
}
