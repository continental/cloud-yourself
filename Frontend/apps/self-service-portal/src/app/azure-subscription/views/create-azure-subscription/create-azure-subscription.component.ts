import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { HateoasClient, ViewBase } from 'fancy-ngx-hateoas-client';

@Component({
  selector: 'app-create-azure-subscription',
  templateUrl: './create-azure-subscription.component.html'
})
export class CreateAzureSubscriptionComponent extends ViewBase {

  _showSpinner = false;

  constructor(private router: Router, activatedRoute: ActivatedRoute, hateoasClient: HateoasClient) { 
    super(activatedRoute, hateoasClient);
  }
  
  _onSave() {
    if(this.viewModel) {
      this._showSpinner = true;
      this.viewModel.create().then((resourceUrl: string) => {
        this.router.navigate(['/azure-subscriptions/show', resourceUrl[0]]);
      });
    } 
  }

}
