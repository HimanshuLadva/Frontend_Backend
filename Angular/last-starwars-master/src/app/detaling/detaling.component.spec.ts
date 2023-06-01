import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DetalingComponent } from './detaling.component';

describe('DetalingComponent', () => {
  let component: DetalingComponent;
  let fixture: ComponentFixture<DetalingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DetalingComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DetalingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
