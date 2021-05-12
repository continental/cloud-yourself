import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MaterializeUiCoreModule } from 'ngx-materialize-ui-core';
import { AzureSubscriptionRoutingModule } from './azure-subscription-routing.module';
import { CreateAzureSubscriptionComponent } from './views/create-azure-subscription/create-azure-subscription.component';
import { ShowAzureSubscriptionComponent } from './views/show-azure-subscription/show-azure-subscription.component'

@NgModule({
  imports: [
    CommonModule,
    FormsModule, 
    MaterializeUiCoreModule,
    AzureSubscriptionRoutingModule
  ],
  declarations: [
    CreateAzureSubscriptionComponent,
    ShowAzureSubscriptionComponent
  ]
})
export class AzureSubscriptionModule { }
