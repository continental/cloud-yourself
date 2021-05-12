import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { CloudAccountRoutingModule } from './cloud-account-routing.module';
import { ListCloudAccountsComponent } from './views/list-cloud-accounts/list-cloud-accounts.component';
import { RequestCloudAccountComponent } from './views/request-cloud-account/request-cloud-account.component';
import { EditCloudAccountComponent } from './views/edit-cloud-account/edit-cloud-account.component';
import { ShowCloudAccountComponent } from './views/show-cloud-account/show-cloud-account.component';
import { MaterializeUiCoreModule } from 'ngx-materialize-ui-core';
import { CloudAccountBaseDataComponent } from './presenters/cloud-account-base-data/cloud-account-base-data.component';

@NgModule({
  imports: [
    CommonModule, 
    FormsModule,
    MaterializeUiCoreModule,
    CloudAccountRoutingModule
  ],
  declarations: [
    ListCloudAccountsComponent,
    RequestCloudAccountComponent,
    EditCloudAccountComponent,
    ShowCloudAccountComponent,
    CloudAccountBaseDataComponent
  ]
})
export class CloudAccountModule { }
