import { Component } from '@angular/core';
import { ViewBase } from 'fancy-ngx-hateoas-client';
import { CloudAccountBaseDataComponent } from '../../presenters/cloud-account-base-data/cloud-account-base-data.component';

@Component({
  selector: 'app-edit-cloud-account',
  templateUrl: './edit-cloud-account.component.html'
})
export class EditCloudAccountComponent extends ViewBase {

  onSaveBaseData(baseDataComponent: CloudAccountBaseDataComponent) {
    this.viewModel.baseData.update();
  }
}
