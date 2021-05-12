import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MaterializeUiCoreModule } from 'ngx-materialize-ui-core';
import { TenantRoutingModule } from './tenant-routing.module';
import { SharedModule } from '../shared/shared.module';
import { ListTenantsComponent } from './views/list-tenants/list-tenants.component';
import { ShowTenantComponent } from './views/show-tenant/show-tenant.component';
import { CreateTenantComponent } from './views/create-tenant/create-tenant.component';
import { EditTenantComponent } from './views/edit-tenant/edit-tenant.component';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    MaterializeUiCoreModule,
    TenantRoutingModule,
    SharedModule
  ],
  declarations: [
    ListTenantsComponent,
    ShowTenantComponent,
    CreateTenantComponent,
    EditTenantComponent
  ]
})
export class TenantModule { }
