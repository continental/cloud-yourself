import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CreateAzureManagedResourceDeploymentComponent } from './views/create-azure-managed-resource-deployment/create-azure-managed-resource-deployment.component';
import { DisplayAzureManagedResourceDeploymentComponent } from './views/display-azure-managed-resource-deployment/display-azure-managed-resource-deployment.component';

const routes: Routes = [{
  path: 'create/azure/:url',
  component: CreateAzureManagedResourceDeploymentComponent
}, {
  path: 'display/azure/:url',
  component: DisplayAzureManagedResourceDeploymentComponent
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ManagedResourceDeploymentRoutingModule { }
