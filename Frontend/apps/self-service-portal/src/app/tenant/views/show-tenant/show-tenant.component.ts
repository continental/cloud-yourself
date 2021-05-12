import { Component } from '@angular/core';
import { ResourceBase, ViewBase } from 'fancy-ngx-hateoas-client';

@Component({
  selector: 'app-show-tenant',
  templateUrl: './show-tenant.component.html'
})
export class ShowTenantComponent extends ViewBase {

  _onApproveCloudAccount(cloudAccount: ResourceBase) {
    cloudAccount._tmp = { disableApprove: true };
    cloudAccount.approve().then(() => {
      this.reloadViewModel();
    });
  }

}
