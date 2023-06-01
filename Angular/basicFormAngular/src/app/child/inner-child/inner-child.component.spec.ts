import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InnerChildComponent } from './inner-child.component';

describe('InnerChildComponent', () => {
  let component: InnerChildComponent;
  let fixture: ComponentFixture<InnerChildComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ InnerChildComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(InnerChildComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
