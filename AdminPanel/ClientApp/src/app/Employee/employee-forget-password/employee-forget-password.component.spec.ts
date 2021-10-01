import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EmployeeForgetPasswordComponent } from './employee-forget-password.component';

describe('EmployeeForgetPasswordComponent', () => {
  let component: EmployeeForgetPasswordComponent;
  let fixture: ComponentFixture<EmployeeForgetPasswordComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EmployeeForgetPasswordComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EmployeeForgetPasswordComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
