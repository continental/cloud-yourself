import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MaterializeUiCoreModule } from 'ngx-materialize-ui-core';
import { PayerAccountRoutingModule } from './payer-account-routing.module';
import { SharedModule } from '../shared/shared.module';
import { ListPayerAccountsComponent } from './views/list-payer-accounts/list-payer-accounts.component';
import { DisplayPayerAccountComponent } from './views/display-payer-account/display-payer-account.component';
import { CreatePayerAccountComponent } from './views/create-payer-account/create-payer-account.component';
import { EditPayerAccountComponent } from './views/edit-payer-account/edit-payer-account.component';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    MaterializeUiCoreModule,
    PayerAccountRoutingModule,
    SharedModule
  ],
  declarations: [
    ListPayerAccountsComponent,
    DisplayPayerAccountComponent,
    CreatePayerAccountComponent,
    EditPayerAccountComponent
  ]
})
export class PayerAccountModule { }
