/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { CloudAccountBaseDataComponent } from './cloud-account-base-data.component';

describe('CloudAccountBaseDataComponent', () => {
  let component: CloudAccountBaseDataComponent;
  let fixture: ComponentFixture<CloudAccountBaseDataComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CloudAccountBaseDataComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CloudAccountBaseDataComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
