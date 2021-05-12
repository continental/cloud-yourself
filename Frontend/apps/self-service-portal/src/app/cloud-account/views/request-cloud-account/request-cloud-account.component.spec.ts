/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { RequestCloudAccountComponent } from './request-cloud-account.component';

describe('RequestCloudAccountComponent', () => {
  let component: RequestCloudAccountComponent;
  let fixture: ComponentFixture<RequestCloudAccountComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RequestCloudAccountComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RequestCloudAccountComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
