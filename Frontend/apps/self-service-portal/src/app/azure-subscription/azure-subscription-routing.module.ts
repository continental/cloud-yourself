import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AddSubscriptionComponent } from './views/add-subscription/add-subscription.component';
import { ShowAzureSubscriptionComponent } from './views/show-azure-subscription/show-azure-subscription.component';

const routes: Routes = [{
  path: 'add/:url',
  component: AddSubscriptionComponent
}, {
  path: 'show/:url',
  component: ShowAzureSubscriptionComponent
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AzureSubscriptionRoutingModule { }
