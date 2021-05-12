import { Component, OnInit } from '@angular/core';
import { ViewBase } from 'fancy-ngx-hateoas-client';

@Component({
  selector: 'app-show-azure-subscription',
  templateUrl: './show-azure-subscription.component.html'
})
export class ShowAzureSubscriptionComponent extends ViewBase {
  _onCancelSubscription() {
    this.viewModel.cancel().then(() => this.reloadViewModel());
  }
}
