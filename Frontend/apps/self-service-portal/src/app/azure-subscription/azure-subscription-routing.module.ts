import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CreateAzureSubscriptionComponent } from './views/create-azure-subscription/create-azure-subscription.component';
import { ShowAzureSubscriptionComponent } from './views/show-azure-subscription/show-azure-subscription.component';

const routes: Routes = [{
  path: 'create/:url',
  component: CreateAzureSubscriptionComponent
}, {
  path: 'show/:url',
  component: ShowAzureSubscriptionComponent
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AzureSubscriptionRoutingModule { }
