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
