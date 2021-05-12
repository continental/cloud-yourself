import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { EditCloudAccountComponent } from './views/edit-cloud-account/edit-cloud-account.component';
import { ListCloudAccountsComponent } from './views/list-cloud-accounts/list-cloud-accounts.component';
import { RequestCloudAccountComponent } from './views/request-cloud-account/request-cloud-account.component';
import { ShowCloudAccountComponent } from './views/show-cloud-account/show-cloud-account.component';

const routes: Routes = [{
  path: 'list/:url',
  component: ListCloudAccountsComponent
}, {
  path: 'request/:url',
  component: RequestCloudAccountComponent
}, {
  path: 'edit/:url',
  component: EditCloudAccountComponent
}, {
  path: 'show/:url',
  component: ShowCloudAccountComponent
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CloudAccountRoutingModule { }
