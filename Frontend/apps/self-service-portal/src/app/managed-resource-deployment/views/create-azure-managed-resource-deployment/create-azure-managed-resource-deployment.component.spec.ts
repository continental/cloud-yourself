/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { CreateAzureManagedResourceDeploymentComponent } from './create-azure-managed-resource-deployment.component';

describe('CreateAzureManagedResourceDeploymentComponent', () => {
  let component: CreateAzureManagedResourceDeploymentComponent;
  let fixture: ComponentFixture<CreateAzureManagedResourceDeploymentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CreateAzureManagedResourceDeploymentComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateAzureManagedResourceDeploymentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
