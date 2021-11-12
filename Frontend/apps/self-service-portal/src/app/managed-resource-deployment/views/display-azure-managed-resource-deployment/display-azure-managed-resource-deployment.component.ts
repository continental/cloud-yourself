import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { HateoasClient, ResourceBase, ViewBase } from 'fancy-ngx-hateoas-client';

@Component({
  selector: 'app-display-azure-managed-resource-deployment',
  templateUrl: './display-azure-managed-resource-deployment.component.html'
})
export class DisplayAzureManagedResourceDeploymentComponent extends ViewBase {

  constructor(private router: Router, activatedRoute: ActivatedRoute, hateoasClient: HateoasClient) { 
    super(activatedRoute, hateoasClient);
  }

  _onPrepare() {
    this.viewModel.prepare().then(() => {
      this.reloadViewModel();
    }).catch((response: any) => {
      this.viewModel.error = response.error;
      this.viewModel.error.errorAction = 'Prepare';
    });
  }

  _onCommit() {
    this.viewModel.commit().then(() => {
      this.reloadViewModel();
    }).catch((response: any) => {
      this.viewModel.error = response.error;
      this.viewModel.error.errorAction = 'Commit';
    });
  }
  
  _onDelete() {
    this.viewModel.delete().then(() => {
      this.router.navigate(['/managed-resources/list', this.viewModel._links.listManagedResourcesVm.href]);
    });
  }

}
