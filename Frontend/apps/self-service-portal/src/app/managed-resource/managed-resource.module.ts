import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MaterializeUiCoreModule } from 'ngx-materialize-ui-core';
import { SharedModule } from '../shared/shared.module';
import { ManagedResourceRoutingModule } from './managed-resource-routing.module';
import { ListManagedResourcesComponent } from './views/list-managed-resources/list-managed-resources.component';
import { CreateAzureManagedResourceComponent } from './views/create-azure-managed-resource/create-azure-managed-resource.component';
import { DisplayAzureManagedResourceComponent } from './views/display-azure-managed-resource/display-azure-managed-resource.component';
import { EditAzureManagedResourceComponent } from './views/edit-azure-managed-resource/edit-azure-managed-resource.component';
@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    MaterializeUiCoreModule,
    ManagedResourceRoutingModule,
    SharedModule
  ],
  declarations: [
    ListManagedResourcesComponent,
    CreateAzureManagedResourceComponent,
    DisplayAzureManagedResourceComponent,
    EditAzureManagedResourceComponent
  ]
})
export class ManagedResourceModule { }
