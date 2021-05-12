/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { EditTenantComponent } from './edit-tenant.component';

describe('EditTenantComponent', () => {
  let component: EditTenantComponent;
  let fixture: ComponentFixture<EditTenantComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EditTenantComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EditTenantComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
