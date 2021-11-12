import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { HateoasClient, ViewBase } from 'fancy-ngx-hateoas-client';

@Component({
  selector: 'app-create-azure-managed-resource-deployment',
  templateUrl: './create-azure-managed-resource-deployment.component.html'
})
export class CreateAzureManagedResourceDeploymentComponent extends ViewBase {

  constructor(private router: Router, activatedRoute: ActivatedRoute, hateoasClient: HateoasClient) { 
    super(activatedRoute, hateoasClient);
  }
  
  _onDeploy() {
    if(this.viewModel) {
      this.viewModel.create().then(() => {
        this.router.navigate(['/azure-subscriptions/show', this.viewModel._links.subscription.href]);
      });
    } 
  }

}
