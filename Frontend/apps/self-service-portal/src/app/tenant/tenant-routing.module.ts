import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ListTenantsComponent } from './views/list-tenants/list-tenants.component';
import { ShowTenantComponent } from './views/show-tenant/show-tenant.component';
import { CreateTenantComponent } from './views/create-tenant/create-tenant.component';
import { EditTenantComponent } from './views/edit-tenant/edit-tenant.component';

const routes: Routes = [{
  path: 'list/:url',
  component: ListTenantsComponent
}, {
  path: 'show/:url',
  component: ShowTenantComponent
}, {
  path: 'create/:url',
  component: CreateTenantComponent
}, {
  path: 'edit/:url',
  component: EditTenantComponent
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class TenantRoutingModule { }
