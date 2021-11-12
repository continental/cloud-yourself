import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CreatePayerAccountComponent } from './views/create-payer-account/create-payer-account.component';
import { DisplayPayerAccountComponent } from './views/display-payer-account/display-payer-account.component';
import { EditPayerAccountComponent } from './views/edit-payer-account/edit-payer-account.component';
import { ListPayerAccountsComponent } from './views/list-payer-accounts/list-payer-accounts.component';

const routes: Routes = [{
  path: 'list/:url',
  component: ListPayerAccountsComponent
}, {
  path: 'display/:url',
  component: DisplayPayerAccountComponent
}, {
  path: 'edit/:url',
  component: EditPayerAccountComponent
}, {
  path: 'create/:url',
  component: CreatePayerAccountComponent
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PayerAccountRoutingModule { }
