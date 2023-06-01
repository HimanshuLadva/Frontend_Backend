import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserComponent } from './user.component';

describe('UserComponent', () => {
  let component: UserComponent;
  let fixture: ComponentFixture<UserComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UserComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UserComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  // property testing
  it('this is component name should be this', () => {
    expect(component.componentName).toBe("Hello this is my user component");
  });

  // function testing
  it("this function should return", () => {
    expect(component.sum(20,20)).toBe(40);
  });

  // html element testing
  it("html element testing", () => {
    const collection = fixture.nativeElement;
    expect(collection.querySelector('.user_component')?.textContent).toContain('Hello');  
  });
});
