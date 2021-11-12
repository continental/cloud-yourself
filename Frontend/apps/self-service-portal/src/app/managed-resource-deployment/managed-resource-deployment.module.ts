import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CreateAzureManagedResourceDeploymentComponent } from './views/create-azure-managed-resource-deployment/create-azure-managed-resource-deployment.component';
import { FormsModule } from '@angular/forms';
import { MaterializeUiCoreModule } from 'ngx-materialize-ui-core';
import { ManagedResourceDeploymentRoutingModule } from './managed-resource-deployment-routing.module';
import { SharedModule } from '../shared/shared.module';
import { DisplayAzureManagedResourceDeploymentComponent } from './views/display-azure-managed-resource-deployment/display-azure-managed-resource-deployment.component';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    MaterializeUiCoreModule,
    ManagedResourceDeploymentRoutingModule,
    SharedModule
  ],
  declarations: [
    CreateAzureManagedResourceDeploymentComponent, 
    DisplayAzureManagedResourceDeploymentComponent]
})
export class ManagedResourceDeploymentModule { }
