import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { HateoasClient, ResourceBase, ViewBase } from 'fancy-ngx-hateoas-client';

@Component({
  selector: 'app-add-subscription',
  templateUrl: './add-subscription.component.html'
})
export class AddSubscriptionComponent extends ViewBase {

  _showSpinner = false;

  _selectedSubscriptionId: string | null = null;

  _selectedSubscription: ResourceBase | null = null;

  constructor(private router: Router, activatedRoute: ActivatedRoute, hateoasClient: HateoasClient) { 
    super(activatedRoute, hateoasClient);
  }

  set selectedSubscriptionId(value: string) {
    this._selectedSubscriptionId = value;
    // Find the subscription out of the options
    this._selectedSubscription = this.viewModel.unmanagedSubscriptions.find((s: any) => s.subscriptionId === this._selectedSubscriptionId);
  }

  get selectedSubscriptionId(): string {
    return this._selectedSubscriptionId ?? '';
  }
  
  _onCreate() {
    if(this.viewModel) {
      this._showSpinner = true;
      this.viewModel.newTemplate.create().then((resourceUrl: string) => {
        this.router.navigate(['/azure-subscriptions/show', resourceUrl[0]]);
      });
    } 
  }

  _onAdd() {
    if(this._selectedSubscription) {
      this._showSpinner = true;
      this._selectedSubscription.add().then((resourceUrl: string) => {
        this.router.navigate(['/azure-subscriptions/show', resourceUrl[0]]);
      });
    } 
  }

}

