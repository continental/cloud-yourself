import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CreateAzureManagedResourceComponent } from './views/create-azure-managed-resource/create-azure-managed-resource.component';
import { DisplayAzureManagedResourceComponent } from './views/display-azure-managed-resource/display-azure-managed-resource.component';
import { EditAzureManagedResourceComponent } from './views/edit-azure-managed-resource/edit-azure-managed-resource.component';
import { ListManagedResourcesComponent } from './views/list-managed-resources/list-managed-resources.component';

const routes: Routes = [{
  path: 'list/:url',
  component: ListManagedResourcesComponent
}, {
  path: 'display/azure/:url',
  component: DisplayAzureManagedResourceComponent
}, {
  path: 'edit/azure/:url',
  component: EditAzureManagedResourceComponent
}, {
  path: 'create/azure/:url',
  component: CreateAzureManagedResourceComponent
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ManagedResourceRoutingModule { }
