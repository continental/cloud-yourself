/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { DisplayAzureManagedResourceDeploymentComponent } from './display-azure-managed-resource-deployment.component';

describe('DisplayAzureManagedResourceDeploymentComponent', () => {
  let component: DisplayAzureManagedResourceDeploymentComponent;
  let fixture: ComponentFixture<DisplayAzureManagedResourceDeploymentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DisplayAzureManagedResourceDeploymentComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DisplayAzureManagedResourceDeploymentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
