import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AddAwsAccountComponent } from './views/add-aws-account/add-aws-account.component';
import { ShowAwsAccountComponent } from './views/show-aws-account/show-aws-account.component';
import { AwsAccountRoutingModule } from './aws-account-routing.module';
import { FormsModule } from '@angular/forms';
import { MaterializeUiCoreModule } from 'ngx-materialize-ui-core';

@NgModule({
  imports: [
    CommonModule,
    FormsModule, 
    MaterializeUiCoreModule,
    AwsAccountRoutingModule
  ],
  declarations: [
    AddAwsAccountComponent,
    ShowAwsAccountComponent
  ]
})
export class AwsAccountModule { }
