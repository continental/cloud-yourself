import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './core/components/home/home.component';

const routes: Routes = [{
  path: 'home',
  component: HomeComponent
}, {
  path: 'tenants',
  loadChildren: () => import('./tenant/tenant.module').then(m => m.TenantModule)
}, {
  path: 'cloud-accounts',
  loadChildren: () => import('./cloud-account/cloud-account.module').then(m => m.CloudAccountModule)
}, {
  path: 'azure-subscriptions',
  loadChildren: () => import('./azure-subscription/azure-subscription.module').then(m => m.AzureSubscriptionModule)
}, {
  path: 'aws-accounts',
  loadChildren: () => import('./aws-account/aws-account.module').then(m => m.AwsAccountModule)
}, {
  path: 'managed-resources',
  loadChildren: () => import('./managed-resource/managed-resource.module').then(m => m.ManagedResourceModule)
}, {
  path: 'managed-resource-deployments',
  loadChildren: () => import('./managed-resource-deployment/managed-resource-deployment.module').then(m => m.ManagedResourceDeploymentModule)
}, {
  path: 'payer-accounts',
  loadChildren: () => import('./payer-account/payer-account.module').then(m => m.PayerAccountModule)
}, {
  path: 'allocation-keys',
  loadChildren: () => import('./allocation-key/allocation-key.module').then(m => m.AllocationKeyModule)
}, {
  path: 'costs',
  loadChildren: () => import('./cost/cost.module').then(m => m.CostModule)
}, {
  path: '**',
  redirectTo: 'home'
}, {
  path: '',
  pathMatch: 'full',
  redirectTo: 'home'
}];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
